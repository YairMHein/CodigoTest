using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMSAPI.DBContext;
using CMSAPI.Models;

namespace CMSAPI.Business
{
    public class EVoucherRepository
    {
        MyDbContext myDbContext;
        public List<EVoucher> GetAll()
        {
            myDbContext = new MyDbContext();
            List<EVoucher> lst = new List<EVoucher>();
            try
            {
                lst = myDbContext.tb_evoucher.ToList();
                return lst;
            }
            catch(Exception e)
            {
                return lst;
            }
           
            
        }

        public ResponseMessage Save(EVoucher obj)
        {
            ResponseMessage response = new ResponseMessage();
            try
            {
                myDbContext.tb_evoucher.Add(obj);
                myDbContext.SaveChanges();
                response.Message = "Save Successful";
                response.MessageType = 1;
            }
            catch (Exception e)
            {
                response.Message = "Save Failed";
                response.MessageType = 2;
            }
            return response;
        }

        public EVoucher Get(int id)
        {
            EVoucher model = new EVoucher();
            model = myDbContext.tb_evoucher.Where(x => x.id == id).FirstOrDefault();
            return model;
        }

        public List<EVoucher> GetAllPaymentMethod()
        {
            List<EVoucher> lst = new List<EVoucher>();
            lst = myDbContext.tb_evoucher.ToList();
            List<EVoucher> lstpay = lst.GroupBy(p => p.paymentmethod).Select(g => g.First()).ToList();
            return lstpay;
        }

        public ResponseMessage MakePayment(EVoucher obj)
        {
            ResponseMessage response = new ResponseMessage();
            using var transaction = myDbContext.Database.BeginTransaction();
            try
            {
                string buyerid = Guid.NewGuid().ToString();

                Buyer obuyer = new Buyer();
                obuyer.id = buyerid;
                obuyer.name = obj.name;
                obuyer.phone = obj.phone;
                myDbContext.tb_buyer.Add(obuyer);

                int evoucherid = obj.id;
                Purchase oPurchase = new Purchase();
                oPurchase.evoucherid = evoucherid;
                oPurchase.buyerid = buyerid;
                oPurchase.buydate = DateTime.Now;
                oPurchase.buyquantity =obj.buyquantity;
                oPurchase.totalamount =obj.amount * obj.buyquantity;
                oPurchase.promocode = null;
                myDbContext.tb_purchase.Add(oPurchase);

                //myDbContext.tb_evoucher.First(x => x.id == evoucherid).buyerid = buyerid;

                var alphas = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                var numers = obj.phone;
                var random = new Random();
                var resultalpha = new string(
                    Enumerable.Repeat(alphas, 5)
                              .Select(s => s[random.Next(s.Length)])
                              .ToArray());
                var resultnumer = new string(
                    Enumerable.Repeat(numers, 6)
                              .Select(s => s[random.Next(s.Length)])
                              .ToArray());
                var result = resultalpha + resultnumer;

                string promocode = new string(result.ToCharArray().
                OrderBy(s => (random.Next(2) % 2) == 0).ToArray());

                myDbContext.tb_evoucher.First(x => x.id == evoucherid).quantity = obj.quantity - obj.buyquantity;

                myDbContext.SaveChanges();
                transaction.Commit();

                response.Promocode = promocode;
                response.Message = "Payment Successful";
                response.MessageType = 1;
            }
            catch (Exception e)
            {
                transaction.Rollback();

                response.Message = "Payment Failed";
                response.MessageType = 2;
            }
            return response;
        }

        public ResponseMessage VerifyPromoCode(EVoucher obj)
        {
            ResponseMessage response = new ResponseMessage();
            try
            {
                var query = myDbContext.tb_purchase.Where(x => x.evoucherid == obj.id && x.buyerid == obj.buyerid).FirstOrDefault();
                if (obj.promocode == query.promocode)
                {
                    response.Message = "Successful";
                    response.MessageType = 1;
                }
                else
                {
                    response.Message = "Incorrect code";
                    response.MessageType = 1;
                }
                
            }
            catch (Exception e)
            {
                response.Message = "Verification Failed";
                response.MessageType = 2;
            }
            return response;
        }

        public List<Purchase> PurchaseHistory(bool isused)
        {
            List<Purchase> lst = new List<Purchase>();
            if (isused)
            {
                lst = (from data in myDbContext.tb_evoucher
                             join pur in myDbContext.tb_purchase on data.id equals pur.evoucherid

                       join buy in myDbContext.tb_buyer on pur.buyerid equals buy.id
                       where data.quantity==0
                             select new Purchase()
                             {
                                 id=pur.id,
                                 evoucherid = data.id,
                                 buyerid = pur.buyerid,
                                 buydate = pur.buydate,
                                 buyquantity = pur.buyquantity,
                                 totalamount = pur.totalamount,
                                 name = buy.name,
                                 title = data.title,
                                 amount = data.amount,
                             }).ToList();
            }
            else{
                lst = (from data in myDbContext.tb_evoucher
                             join pur in myDbContext.tb_purchase on data.id equals pur.evoucherid
                       join buy in myDbContext.tb_buyer on pur.buyerid equals buy.id
                       where data.quantity != 0
                             select new Purchase()
                             {
                                 id = pur.id,
                                 evoucherid = data.id,
                                 buyerid = pur.buyerid,
                                 buydate = pur.buydate,
                                 buyquantity = pur.buyquantity,
                                 totalamount = pur.totalamount,
                                 name = buy.name,
                                 title = data.title,
                                 amount = data.amount,
                             }).ToList();
            }
            return lst;
        }
    }
}

using Business.base_class;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.singleton;
using Data;

namespace Business.object_class
{
    public class cls_login:base_class.business_base_class
    {
        private string user;
        private string pass;
        private long id;
        private string phone;
        private string mail;
        private List<cls_rol> roles;
        private long idCustomer=0;
        private cls_customer customer = null;

        public string User { get => user; set => user = value; }
        public string Pass { get => pass; set => pass = value; }
        public long Id { get => id; set => id = value; }
        public string Phone { get => phone; set => phone = value; }
        public string Mail { get => mail; set => mail = value; }
        public List<cls_rol> Roles { get => roles; set => roles = value; }
        public long IdCustomer { get => idCustomer; set => idCustomer = value; }
        public cls_customer Customer { get => customer; set => customer = value; }

        public cls_login() { }

        public cls_login Login()
        {
            ObjectResult<bool?> result = cls_static_MksModel.GetEntity().usp_CheckLogin(User,Pass);
            if (result.ToArray()[0].Value == true)
            {
                long idLogin = cls_static_MksModel.GetEntity().usp_GetLogin(User, Pass).ToArray()[0].Value;
                pass = String.Empty;
                id = idLogin;
                List<long?> resCustomer = cls_static_MksModel.GetEntity().usp_selectSellerNyIdLogin(idLogin).ToList();
                if (resCustomer.Count > 0) { 
                IdCustomer = resCustomer[0] != null ? resCustomer[0].Value : 0;
                    customer = cls_customer.GetCustomerByInternalID(IdCustomer);
                }


                Token = new cls_token(cls_static_MksModel.GetEntity().usp_InsertToken(idLogin).ToList()[0]);
                return this;
            }
            else 
            {
                return null;
            }
            
            
        }

        public cls_login(usp_GetAllLogins_Result r, long companyid) 
        {
            user = r.LOGIN;
            Id = r.ID;
            phone = r.PHONE;
            mail = r.EMAIL;
            roles = new List<cls_rol>();
            roles  = cls_rol.GetRolesByUserId(Id, companyid);
            //roles = cls_rol.
        }

        public cls_login(usp_GetLoginById_Result r)
        {
            user = r.login;
            id = r.id;
            phone = r.phone;
            mail = r.email;
            roles = GetRolesById(r.id);
        }

        public static long GetCompanyIdByIdLogin(long idlogin) 
        {
            return cls_static_MksModel.GetEntity().usp_GetLoginById(idlogin).ToList()[0].id_company;
        }

        public static filter_paged_response insertLogin(cls_login r, long par_company) 
        {
            filter_paged_response resp = new filter_paged_response();
            MSKEntities msk = Data.singleton.cls_static_MksModel.GetEntity();
            cls_login l = r;
            l.Phone = l.Phone == null ? "" : l.phone;
            l.Mail = l.Mail == null ? "" : l.mail;
            try
            {
                if (r.roles!=null && r.roles.Count > 0) 
                {
                    resp.Debug.Add("contiene roles");
                }

                long intres = msk.usp_insertLogin(l.User, par_company, l.Mail, l.pass, l.phone, l.IdCustomer).ToList()[0].Value;
                resp.Debug.Add("Usuario creado id:" + intres.ToString());
                if (intres != -1)
                {
                    
                    resp.Debug.Add("OK");
                    resp.Debug.Add(l.Name);
                    resp.Debug.Add("Usuario creado con exito");
                   
                    if (l != null && l.Roles != null && l.Roles.Count > 0)
                    {
                        resp.Debug.Add("Contiene Roles...");
                        resp.Debug.Add("Linkeando roles...");
                        foreach (cls_rol rol in l.roles)
                        {
                            resp.Debug.Add("linkeando ROL " + rol.Id + " a usuario " + intres);
                            
                            MSKEntities ent = Data.singleton.cls_static_MksModel.GetEntity();
                            ent.usp_LinkUserRole(intres, par_company, rol.Id);
                        }
                    }
                }
                else
                {
                    resp.Debug.Add("Error");
                    resp.Debug.Add("Usuario existente o datos incorrectos");

                }
                return resp;
            }
            catch (Exception e)
            {
                resp.Debug.Add(e.Message);
                resp.Debug.Add(e.StackTrace);
                return resp;
            }


        }

        public static filter_paged_response GetLogins(long par_company)
        {
            MSKEntities msk = Data.singleton.cls_static_MksModel.GetEntity();
            List<usp_GetAllLogins_Result> r = msk.usp_GetAllLogins(par_company).ToList();
            filter_paged_response fpr = new filter_paged_response();
            if (r != null && r.Count > 0) 
            {
                List<cls_login> loginList = new List<cls_login>();
                foreach (usp_GetAllLogins_Result i in r) 
                {
                    loginList.Add(new cls_login(i, par_company));
                }
                fpr.LoginList = loginList;
                
            }

            return fpr;
            
        }

        public static cls_login Get(long id)
        {
            MSKEntities msk = cls_static_MksModel.GetEntity();
            ObjectResult<usp_GetLoginById_Result>  r = msk.usp_GetLoginById(id);

            cls_login user = null;

            if (r != null && r.Count() > 0)
            {
                // Obtenemos el primer elemento que deberia ser el usuario seleccionado
                user = new cls_login(r.First());
            }

            return user;
        }

        static List<cls_rol> GetRolesById(long loginid)
        {
            MSKEntities msk = Data.singleton.cls_static_MksModel.GetEntity();
            List<usp_GetRolesByLoginId_Result> r = msk.usp_GetRolesByLoginId(loginid).ToList();

            List<cls_rol> rolelist = new List<cls_rol>();

            if (r != null && r.Count > 0)
            {
                foreach (usp_GetRolesByLoginId_Result rol in r)
                    rolelist.Add(new cls_rol(rol));
            }

            return rolelist;
        }
    }
}

using System;
using B2E.Data;

namespace B2E.Business
{
    public class hitBusiness : utilData
    {
        public string Link(int id)
        {
            hitData hitData = new hitData();
            try
            {
                return hitData.Link(id);
            }
            catch (Exception ex)
            {
                Tratamento(ex.HResult, ex.Message, ex.Source, "hitBusiness.Link(" + id + ")", ex.StackTrace, false, utilData.DB);
            }
            return "";
        }
    }
}
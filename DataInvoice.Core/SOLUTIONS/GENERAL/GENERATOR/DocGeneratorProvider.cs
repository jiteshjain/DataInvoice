using NGLib.COMPONENTS.DOCUMENT.DOCGENERATOR;
using NGLib.COMPONENTS.DOCUMENT.DOCGENERATOR.GENERATOR;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataInvoice.SOLUTIONS.GENERAL.GENERATOR
{
    public class DocGeneratorProvider : NGLib.DATA.DATAPO.DataPOProviderSQL, IDocGeneratorProvider
    {
        public DocGeneratorProvider(NGLib.DATA.CONNECTOR.IDataConnector connector)
            : base(connector)
        {
        }

        public FileInfo GetDefaultOutputFile(IDocGenerator docgenerator)
        {
            throw new NotImplementedException();
        }

        public DataSet ExecuteSql(IDocGenerator docgenerator)
        {
            throw new NotImplementedException();
        }


        public DocGeneratorPO GetDocGenerator(int IDocGenerator)
        {
            Dictionary<string, object> ins = new Dictionary<string, object>();
            ins.Add("IDDocGenerator", IDocGenerator);
            return this.GetOneDefault<DocGeneratorPO>(ins);
        }

        public IDocGenerator GetIDocGenerator(int IDocGenerator)
        {
            Dictionary<string, object> ins = new Dictionary<string, object>();
            ins.Add("IDDocGenerator", IDocGenerator);

            return this.GetOneDefault<DocGeneratorPO>(ins);
        }

        public IDocGenerator GetIDocGeneratorByUniqueLabel(string UniqueLabel)
        {
            Dictionary<string, object> ins = new Dictionary<string, object>();
            ins.Add("UniqueLabel", UniqueLabel);

            string sql = "SELECT TOP 1 * FROM documentgenerator WHERE UniqueLabel = @UniqueLabel";

            System.Data.DataTable res = this.Connector.Query(sql, ins);
            return NGLib.DATA.DATAPO.DataPOParser.OneFromDataTable<DocGeneratorPO>(res);
        }

        public IEnumerable<DocGeneratorPO> GetDocGenerators(int nbmax = 1000)
        {
            return this.GetListAllDefault<DocGeneratorPO>(nbmax);
        }

        public IEnumerable<DocGeneratorPO> GetDocGeneratorsUniqueLabels(int nbmax = 1000)
        {
            string sql = "SELECT UniqueLabel FROM documentgenerator " +
                         "WHERE UniqueLabel IS NOT NULL " +
                         "GROUP BY UniqueLabel ";
            DataTable res = this.Connector.Query(sql);
            return NGLib.DATA.DATAPO.DataPOParser.ListFromDataTable<DocGeneratorPO>(res);
        }


        public void InsertDocGenerator(DocGeneratorPO docGenerator)
        {
            docGenerator["IDDocGenerator"] = DBNull.Value;
            this.InsertBubble(docGenerator, false, false);

        }

        //public DocGeneratorPO CreateDocGenerator(DocGeneratorPoco poco)
        //{
        //    try
        //    {
        //        DocGeneratorPO docGenerator = new DocGeneratorPO();
        //        docGenerator.FromObject(poco);

        //        this.InsertDocGenerator(docGenerator);
        //        if (poco.FluxXml != null)
        //            this.UpdateFlux(docGenerator, poco.FluxXml);

        //        return docGenerator;
        //    }
        //    catch (Exception ex)
        //    {

        //        throw new Exception("CreateDocgenerator: " + ex.Message + " ", ex);
        //    }

        //}

        ///// <summary>
        ///// Récupère l'objet DocgeneratorPO de la base, et applique les modifications renseignées dans 
        ///// le Poco passé en paramètre
        ///// </summary>
        ///// <param name="poco"></param>
        //public DocGeneratorPO UpdateDocGenerator(DocGeneratorPoco poco)
        //{
        //    DocGeneratorPO docGenerator = (DocGeneratorPO)GetIDocGenerator(poco.IDDocGenerator);
        //    if (docGenerator == null)
        //        throw new Exception("UpdateDocGenerator: docgenerator non trouvé ");
        //    docGenerator.FromObject(poco);

        //    this.SaveBubble(docGenerator);
        //    return docGenerator;
        //}

        public void UpdateDocGenerator(DocGeneratorPO docGenerator)
        {
            this.SaveBubble(docGenerator);
        }

        /// <summary>
        /// Met à jour le flux XML du DocGenerator
        /// Génère une erreur si le flux est invalide
        /// </summary>
        /// <param name="docGenerator"></param>
        /// <param name="fluxxml"></param>
        public void UpdateFlux(DocGeneratorPO docGenerator, string fluxxml)
        {
            // si flux invalide générera une erreur
            new NGLib.DATA.DATAVALUES.DataValues().DatavalueManager().fromFluxXML(fluxxml);

            base.UpdateBubble(docGenerator, "Fluxxml", fluxxml);
        }

        public void DeleteDocGenerator(int IDDocGenerator)
        {
            DocGeneratorPO docGenerator = (DocGeneratorPO)GetIDocGenerator(IDDocGenerator);
            if (docGenerator == null)
                throw new Exception("DeleteDocGenerator: docgenerator non trouvé ");

            this.DeleteBubble(docGenerator);
        }


        /// <summary>
        /// Construit l'objet Generator permettant la génération de document en fonction de l'objet DocGenerator
        /// Puis Génère le document en fonction des inputs
        /// </summary>
        /// <param name="docgenerator">param/model de génération</param>
        /// <param name="inputs">Données Inputs en direct</param>
        /// <param name="OutputFile">fichier de sortie</param>
        /// <returns>résultat et fichier de sortie</returns>
        public IDocGeneratorItem Generate(IDocGenerator docgenerator, Dictionary<string, object> inputs = null, FileInfo OutputFile = null)
        {
            if (docgenerator == null)
                throw new Exception("Generate : DocGenerator null ");
            IGeneratorEngine generator = NGLib.COMPONENTS.DOCUMENT.DOCGENERATOR.GeneratorEngineFactory.BuildGeneratorEngine(docgenerator);


            return generator.RunGenerate(docgenerator, inputs, OutputFile);
        }


        public IDocGeneratorItem Generate(IDocGenerator docgenerator, NGLib.DATA.DATAPO.DataPO modelpo, FileInfo OutputFile = null)
        {
            if (docgenerator == null)
                throw new Exception("Generate : DocGenerator null ");
            IGeneratorEngine generator = NGLib.COMPONENTS.DOCUMENT.DOCGENERATOR.GeneratorEngineFactory.BuildGeneratorEngine(docgenerator);


            //if(docgenerator.InputPath.Contains("{!appdirectory}"))
            //    docgenerator.InputPath=docgenerator.InputPath.Replace("{!appdirectory}","");

            if (docgenerator.InputPath.Contains("{!invoiceviewdirectory}"))
            {
                string filename = docgenerator.InputPath.Replace("{!invoiceviewdirectory}", "");
                ((DocGeneratorPO)docgenerator).InputPath = System.Web.HttpContext.Current.Server.MapPath("~/Views/Invoices/"+filename);
            }

            Dictionary<string, object> inputs = new Dictionary<string, object>();
            inputs.Add("model", modelpo);

            return generator.RunGenerate(docgenerator, inputs, OutputFile);
        }






        //public static bool VerifyViewBag(DynamicObject viewbag, string[] requiredObjects)
        ////Dictionary<string,Type> requiredObjects) //EVOL
        //{
        //    bool result = true;
        //    IEnumerable<string> members = viewbag.GetDynamicMemberNames();

        //    foreach (string requiredObject in requiredObjects)
        //    //foreach (KeyValuePair<string, Type> requiredObject in requiredObjects)
        //    {
        //        if (!members.Contains(requiredObject))
        //        {
        //            throw new Exception(String.Format("VerifyViewBag : L'objet {0} n'a pas été passé dans le Viewbag.", requiredObject));
        //            result = false;
        //        }
        //    }

        //    return result;
        //}
    }
}

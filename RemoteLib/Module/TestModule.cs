using Nancy;

namespace RemoteLib.Module
{
    public sealed class TestModule : NancyModule
    {
        public TestModule()
        {
            //return HttpStatusCode.OK;
            //Get("/", x => "Hel11lo World");
            //Get("/GetPerson/{id:int}", parameters =>
            //    {
            //        Person p = new Person();
            //        p.ID = parameters.ID;
            //        p.Name = "张三";
            //        return Response.AsJson(p);
            //    });

            //Get["sample/{Id}"] = parameter =>
            //{
            //    return listener.GetFlowcellBarcodeId(parameter.Id.Value);
            //};
            //Get("/", args => "This is the site home");
            //Get("/rel", args => "This is the site route");
            //Get("/rel/header", args =>
            //{
            //    var response = new Response();
            //    response.Headers["X-Some-Header"] = "Some value";
            //    return response;
            //});
            //Post("/rel", args => new StreamReader(this.Request.Body).ReadToEnd());
            //Get("/exception", args =>
            //{
            //    return new Response
            //    {
            //        Contents = s =>
            //        {
            //            var writer = new StreamWriter(s);
            //            writer.Write("Content");
            //            writer.Flush();
            //            throw new Exception("An error occured during content rendering");
            //        }
            //    };
            //});
        }
    }
}

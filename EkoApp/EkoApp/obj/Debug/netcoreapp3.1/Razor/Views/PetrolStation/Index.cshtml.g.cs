#pragma checksum "C:\Users\lenovo\source\repos\BESTHackingLeague\EkoApp\EkoApp\Views\PetrolStation\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "6c2fe8d89efd35a5833cb8f414cb92a6e7c70fe3"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_PetrolStation_Index), @"mvc.1.0.view", @"/Views/PetrolStation/Index.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\lenovo\source\repos\BESTHackingLeague\EkoApp\EkoApp\Views\_ViewImports.cshtml"
using EkoApp;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\lenovo\source\repos\BESTHackingLeague\EkoApp\EkoApp\Views\_ViewImports.cshtml"
using EkoApp.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6c2fe8d89efd35a5833cb8f414cb92a6e7c70fe3", @"/Views/PetrolStation/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"f32ef510c37b5c227c95e15e7fe78c72cf06cac3", @"/Views/_ViewImports.cshtml")]
    public class Views_PetrolStation_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<EkoApp.ViewsModel.LocalisationModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("    <div class=\"col-12 p-3 offset-1\">\r\n        <div id=\"kk\" style=\"background-color:white;margin-top:5%\" class=\"col-4 p-3 offset-3 rounded\">\r\n            <div class=\"form-group row\"");
            BeginWriteAttribute("style", " style=\"", 225, "\"", 233, 0);
            EndWriteAttribute();
            WriteLiteral(@">
                <div class=""col-3 text-center"">
                    <label>Ile litrów chcesz zatankować?</label>
                </div>
                <div class=""col-6"">
                    <input name=""Volume"" onChange=""onInput();"" type=""text"" id=""inpta"" />
                </div>
            </div>
            <div class=""form-group row"">
                <div class=""col-3 text-center"">
                    <label>Podaj średnie spalanie:</label>
                </div>
                <div class=""col-6"">
                    <input name=""FuelConsumption"" onChange=""onInputFuel();"" type=""text"" id=""inpta2"" />
                </div>
            </div>
            <div class=""col-12 offset-3"">
                <a class=""btn btn-light"" id=""ka"" style=""background-color:aquamarine"">Wyswietl stacje</a>
            </div>
        </div>
    </div>


    <script type=""text/javascript"">
        getLocation();
        var link;
        var vol;
        var fuel;
        function getLocation() {");
            WriteLiteral(@"
            if (navigator.geolocation) {
                navigator.geolocation.getCurrentPosition(showPosition);
            } else {
                console.log(""Geolocation is not supported by this browser."");
            }
        }

        function showPosition(position) {
            link = ""/PetrolStation/ShowNearest?latitude="" + position.coords.latitude + ""&longitude="" + position.coords.longitude + ""&fuelVolume="";
        }

        function onInput() {
            var inpt = document.getElementById('inpta');
            vol = inpt.value;

            if (fuel != null) {
                link2 = link + inpt.value + ""&fuelConsumption="" + fuel;
                var anch = document.getElementById('ka');
                anch.href = link2;
            }
        }

        function onInputFuel() {
            var inpt = document.getElementById('inpta2');
            fuel = inpt.value;

            if (vol != null) {
                link2 = link + vol + ""&fuelConsumption="" + fuel;
");
            WriteLiteral("                var anch = document.getElementById(\'ka\');\r\n                anch.href = link2;\r\n            }\r\n        }\r\n    </script>\r\n    <button id=\"kk\"></button>\r\n");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<EkoApp.ViewsModel.LocalisationModel> Html { get; private set; }
    }
}
#pragma warning restore 1591

#pragma checksum "C:\Users\lenovo\source\repos\BESTHackingLeague\EkoApp\EkoApp\Views\PetrolStation\ShowNearest.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "aad2e094ff1a55322f7fba886eb69629a95852c4"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_PetrolStation_ShowNearest), @"mvc.1.0.view", @"/Views/PetrolStation/ShowNearest.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"aad2e094ff1a55322f7fba886eb69629a95852c4", @"/Views/PetrolStation/ShowNearest.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"f32ef510c37b5c227c95e15e7fe78c72cf06cac3", @"/Views/_ViewImports.cshtml")]
    public class Views_PetrolStation_ShowNearest : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<EkoApp.ViewsModel.PetrolWithData>>
    {
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "C:\Users\lenovo\source\repos\BESTHackingLeague\EkoApp\EkoApp\Views\PetrolStation\ShowNearest.cshtml"
 foreach (var petrol in @Model)
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <div class=\"col-12 border  p-3 mt-3 bg-white\">\r\n        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "aad2e094ff1a55322f7fba886eb69629a95852c43641", async() => {
                WriteLiteral("\r\n\r\n                    <div class=\"text-dark mb-2\">\r\n                       Nazwa stacji: ");
#nullable restore
#line 9 "C:\Users\lenovo\source\repos\BESTHackingLeague\EkoApp\EkoApp\Views\PetrolStation\ShowNearest.cshtml"
                                Write(petrol.Petrol.Name);

#line default
#line hidden
#nullable disable
                WriteLiteral(" | Odległość do stacji: ");
#nullable restore
#line 9 "C:\Users\lenovo\source\repos\BESTHackingLeague\EkoApp\EkoApp\Views\PetrolStation\ShowNearest.cshtml"
                                                                           Write(petrol.Distance);

#line default
#line hidden
#nullable disable
                WriteLiteral(" | Adres stacji: ");
#nullable restore
#line 9 "C:\Users\lenovo\source\repos\BESTHackingLeague\EkoApp\EkoApp\Views\PetrolStation\ShowNearest.cshtml"
                                                                                                            Write(petrol.Petrol.Street);

#line default
#line hidden
#nullable disable
                WriteLiteral(" | Rzeczywista cena za lirt: ");
#nullable restore
#line 9 "C:\Users\lenovo\source\repos\BESTHackingLeague\EkoApp\EkoApp\Views\PetrolStation\ShowNearest.cshtml"
                                                                                                                                                              Write(petrol.RealPricePerLiter);

#line default
#line hidden
#nullable disable
                WriteLiteral(" zł | Cena za litr: ");
#nullable restore
#line 9 "C:\Users\lenovo\source\repos\BESTHackingLeague\EkoApp\EkoApp\Views\PetrolStation\ShowNearest.cshtml"
                                                                                                                                                                                                           Write(petrol.Petrol.Price);

#line default
#line hidden
#nullable disable
                WriteLiteral(" zł\r\n                    </div>\r\n                ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n               </div>\r\n");
#nullable restore
#line 13 "C:\Users\lenovo\source\repos\BESTHackingLeague\EkoApp\EkoApp\Views\PetrolStation\ShowNearest.cshtml"
}

#line default
#line hidden
#nullable disable
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<EkoApp.ViewsModel.PetrolWithData>> Html { get; private set; }
    }
}
#pragma warning restore 1591

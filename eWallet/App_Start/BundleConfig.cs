using System.Web;
using System.Web.Optimization;

namespace eWallet
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            // CSS style (gentelella)
            bundles.Add(new StyleBundle("~/Content/gentelella").Include(
                      "~/Content/css/custom.css"));
            // CSS style (gentelella)
            bundles.Add(new ScriptBundle("~/Vendors/gentelella").Include(
                      "~/Content/js/custom.js"));

            // CSS style (animate)
            bundles.Add(new StyleBundle("~/Content/animate").Include(
                      "~/Content/vendors/animate.css/animate.css"));

            // Script (autosize)
            bundles.Add(new ScriptBundle("~/vendors/autosize").Include(
                      "~/Content/vendors/autosize/dist/autosize.js"));

            // CSS style (bootstrap)
            bundles.Add(new StyleBundle("~/Content/bootstrap").Include(
                      "~/Content/vendors/bootstrap/dist/css/bootstrap.css"));
            // Bootstrap
            bundles.Add(new ScriptBundle("~/Vendors/bootstrap").Include(
                      "~/Content/vendors/bootstrap/dist/js/bootstrap.js"));

            // CSS style (bootstrap-daterangepicker)
            bundles.Add(new StyleBundle("~/Content/bootstrap-daterangepicker").Include(
                      "~/Content/vendors/bootstrap-daterangepicker/daterangepicker.css"));
            // bootstrap-daterangepicker
            bundles.Add(new ScriptBundle("~/Vendors/bootstrap-daterangepicker").Include(
                      "~/Content/vendors/bootstrap-daterangepicker/daterangepicker.js"));

            // CSS style (bootstrap-progressbar)
            bundles.Add(new StyleBundle("~/Content/bootstrap-progressbar").Include(
                      "~/Content/vendors/bootstrap-progressbar/css/bootstrap-progressbar-3.3.4.min.css"));
            // bootstrap-progressbar
            bundles.Add(new ScriptBundle("~/Vendors/bootstrap-progressbar").Include(
                      "~/Content/vendors/bootstrap-progressbar/bootstrap-progressbar.js"));

            // CSS style (bootstrap-wysiwyg)
            bundles.Add(new StyleBundle("~/Content/bootstrap-wysiwyg").Include(
                      "~/Content/vendors/bootstrap-wysiwyg/css/style.css",
                      "~/Content/vendors/google-code-prettify/bin/prettify.min.css"));
            // bootstrap-wysiwyg
            bundles.Add(new ScriptBundle("~/Vendors/bootstrap-wysiwyg").Include(
                      "~/Content/vendors/bootstrap-wysiwyg/src/bootstrap-wysiwyg.js",
                      "~/Content/vendors/jquery.hotkeys/jquery.hotkeys.js",
                      "~/Content/vendors/google-code-prettify/src/prettify.js"));

            // Chart.js
            bundles.Add(new ScriptBundle("~/Vendors/Chartjs").Include(
                      "~/Content/vendors/Chart.js/dist/Chart.js"));

            // CSS style (cropper)
            bundles.Add(new StyleBundle("~/Content/cropper").Include(
                      "~/Content/vendors/cropper/dist/cropper.css"));
            // cropper
            bundles.Add(new ScriptBundle("~/Vendors/cropper").Include(
                      "~/Content/vendors/cropper/dist/cropper.js"));

            // CSS style (datatable)
            bundles.Add(new StyleBundle("~/Content/datatable").Include(
                      "~/Content/vendors/datatables.net-bs/css/dataTables.bootstrap.css",
                      "~/Content/vendors/datatables.net-buttons-bs/css/buttons.bootstrap.min.css",
                      "~/Content/vendors/datatables.net-fixedheader-bs/css/fixedHeader.bootstrap.min.css",
                      "~/Content/vendors/datatables.net-responsive-bs/css/responsive.bootstrap.min.css",
                      "~/Content/vendors/datatables.net-scroller-bs/css/scroller.bootstrap.min.css"));
            // Datatable
            bundles.Add(new ScriptBundle("~/vendors/datatable").Include(
                      "~/Content/vendors/datatables.net/js/jquery.dataTables.min.js",
                      "~/Content/vendors/datatables.net-bs/js/dataTables.bootstrap.min.js",
                      "~/Content/vendors/datatables.net-buttons/js/dataTables.buttons.min.js",
                      "~/Content/vendors/datatables.net-buttons-bs/js/buttons.bootstrap.min.js",
                      "~/Content/vendors/datatables.net-buttons/js/buttons.flash.min.js",
                      "~/Content/vendors/datatables.net-buttons/js/buttons.html5.min.js",
                      "~/Content/vendors/datatables.net-buttons/js/buttons.print.min.js",
                      "~/Content/vendors/datatables.net-fixedheader/js/dataTables.fixedHeader.min.js",
                      "~/Content/vendors/datatables.net-keytable/js/dataTables.keyTable.min.js",
                      "~/Content/vendors/datatables.net-responsive/js/dataTables.responsive.min.js",
                      "~/Content/vendors/datatables.net-responsive-bs/js/responsive.bootstrap.js",
                      "~/Content/vendors/datatables.net-scroller/js/dataTables.scroller.min.js"));

            // DateJS
            bundles.Add(new ScriptBundle("~/vendors/DateJS").Include(
                     "~/Content/vendors/DateJS/build/date.js"));

            //Todo: AutoComplete

            // CSS style (dropzone)
            bundles.Add(new StyleBundle("~/Content/dropzone").Include(
                      "~/Content/vendors/dropzone/dist/min/dropzone.min.css"));
            // dropzone
            bundles.Add(new ScriptBundle("~/Vendors/dropzone").Include(
                      "~/Content/vendors/dropzone/dist/min/dropzone.min.js"));

            // ECharts
            bundles.Add(new ScriptBundle("~/Vendors/echarts").Include(
                      "~/Content/vendors/echarts/dist/echarts.min.js",
                      "~/Content/vendors/echarts/map/js/world.js"));

            // eve
            bundles.Add(new ScriptBundle("~/Vendors/eve").Include(
                      "~/Content/vendors/eve/eve.js"));

            // fastclick
            bundles.Add(new ScriptBundle("~/vendors/fastclick").Include(
                      "~/Content/vendors/fastclick/lib/fastclick.js"));

            // Flot
            bundles.Add(new ScriptBundle("~/Vendors/flot").Include(
                      "~/Content/vendors/Flot/jquery.flot.js",
                      "~/Content/vendors/Flot/jquery.flot.pie.js",
                      "~/Content/vendors/Flot/jquery.flot.time.js",
                      "~/Content/vendors/Flot/jquery.flot.stack.js",
                      "~/Content/vendors/Flot/jquery.flot.resize.js",
                      "~/Content/vendors/flot.orderbars/js/jquery.flot.orderBars.js",
                      "~/Content/vendors/flot-spline/js/jquery.flot.spline.min.js",
                      "~/Content/vendors/flot.curvedlines/curvedLines.js"));

            // Font Awesome icons
            bundles.Add(new StyleBundle("~/Content/font-awesome").Include(
                     "~/Content/vendors/font-awesome/css/font-awesome.min.css"));

            // CSS style (fullCalendar)
            bundles.Add(new StyleBundle("~/Content/fullCalendar").Include(
                      "~/Content/vendors/fullcalendar/dist/fullcalendar.css",
                      "~/Content/vendors/fullcalendar/dist/fullcalendar.print.css"));
            // fullCalendar
            bundles.Add(new ScriptBundle("~/Vendors/fullCalendar").Include(
                      "~/Content/vendors/fullcalendar/dist/fullcalendar.js"));

            // gauge.js
            bundles.Add(new ScriptBundle("~/Vendors/gaugejs").Include(
                      "~/Content/vendors/gauge.js/dist/gauge.min.js"));

            // google-code-prettify : See bootstrap-wysiwyg

            // CSS style (iCheck)
            bundles.Add(new StyleBundle("~/Content/iCheck").Include(
                      "~/Content/vendors/iCheck/skins/flat/green.css"));
            // iCheck
            bundles.Add(new ScriptBundle("~/Vendors/iCheck").Include(
                      "~/Content/vendors/iCheck/icheck.min.js"));

            // CSS style (Ion.RangeSlider)
            bundles.Add(new StyleBundle("~/Content/ion.rangeSlider").Include(
                      "~/Content/vendors/normalize-css/normalize.css",
                      "~/Content/vendors/ion.rangeSlider/css/ion.rangeSlider.css",
                      "~/Content/vendors/ion.rangeSlider/css/ion.rangeSlider.skinFlat.css"));
            // Ion.RangeSlider
            bundles.Add(new ScriptBundle("~/Vendors/ion.rangeSlider").Include(
                      "~/Content/vendors/ion.rangeSlider/js/ion.rangeSlider.min.js"));

            // jQuery
            bundles.Add(new ScriptBundle("~/Vendors/jquery").Include(
                        "~/Content/vendors/jquery/dist/jquery.js"));

            // jQuery Knob
            bundles.Add(new ScriptBundle("~/Vendors/jquery-knob").Include(
                        "~/Content/vendors/jquery-knob/dist/jquery.knob.min.js"));

            // Todo: jquery-mousewheel

            // jQuery smart-wizard
            bundles.Add(new ScriptBundle("~/Vendors/jQuery-smart-wizard").Include(
                        "~/Content/vendors/jQuery-Smart-Wizard/js/jquery.smartWizard.js"));

            // jQuery Sparklines
            bundles.Add(new ScriptBundle("~/Vendors/jquery-sparkline").Include(
                       "~/Content/vendors/jquery-sparkline/dist/jquery.sparkline.min.js"));

            // jquery-validate
            bundles.Add(new ScriptBundle("~/Vendors/jquery-validate").Include(
                      "~/Content/vendors/jquery-validate/jquery.validate.min.js",
                      "~/Content/vendors/jquery-validate/jquery.validate.unobtrusive.min.js",
                      "~/Content/vendors/jquery-validate/CustomClassValidation.js"));

            // jquery.easy-pie-chart
            bundles.Add(new ScriptBundle("~/Vendors/jquery.easy-pie-chart").Include(
                       "~/Content/vendors/jquery.easy-pie-chart/dist/jquery.easypiechart.min.js"));

            // jquery.hotkeys
            bundles.Add(new ScriptBundle("~/Vendors/jquery.hotkeys").Include(
                       "~/Content/vendors/jquery.hotkeys/jquery.hotkeys.js"));

            // jquery.inputmask
            bundles.Add(new ScriptBundle("~/Vendors/jquery.inputmask").Include(
                       "~/Content/vendors/jquery.inputmask/dist/min/jquery.inputmask.bundle.min.js"));

            // CSS style (jquery.tagsinput)
            bundles.Add(new StyleBundle("~/Content/jquery.tagsinput").Include(
                      "~/Content/vendors/jquery.tagsinput/src/jquery.tagsinput.css"));
            // jquery.tagsinput
            bundles.Add(new ScriptBundle("~/Vendors/jquery.tagsinput").Include(
                       "~/Content/vendors/jquery.tagsinput/src/jquery.tagsinput.js"));

            // CSS style (jqvmap)
            bundles.Add(new StyleBundle("~/Content/jqvmap").Include(
                      "~/Content/vendors/jqvmap/dist/jqvmap.min.css"));
            // jqvmap
            bundles.Add(new ScriptBundle("~/Vendors/jqvmap").Include(
                      "~/Content/vendors/jqvmap/dist/jquery.vmap.js",
                      "~/Content/vendors/jqvmap/dist/maps/jquery.vmap.world.js",
                      "~/Content/vendors/jqvmap/examples/js/jquery.vmap.sampledata.js"));

            // jszip
            bundles.Add(new ScriptBundle("~/vendors/jszip").Include(
                      "~/Content/vendors/jszip/dist/jszip.min.js"));

            // CSS style (custom-scrollbar-plugin)
            bundles.Add(new StyleBundle("~/Content/custom-scrollbar-plugin").Include(
                      "~/Content/vendors/malihu-custom-scrollbar-plugin/jquery.mCustomScrollbar.css"));
            // custom-scrollbar-plugin
            bundles.Add(new ScriptBundle("~/Vendors/custom-scrollbar-plugin").Include(
                       "~/Content/vendors/malihu-custom-scrollbar-plugin/jquery.mCustomScrollbar.js"));

            // CSS style (bootstrap-colorpicker)
            bundles.Add(new StyleBundle("~/Content/bootstrap-colorpicker").Include(
                      "~/Content/vendors/mjolnic-bootstrap-colorpicker/dist/css/bootstrap-colorpicker.min.css"));
            // bootstrap-colorpicker
            bundles.Add(new ScriptBundle("~/Vendors/bootstrap-colorpicker").Include(
                       "~/Content/vendors/mjolnic-bootstrap-colorpicker/dist/js/bootstrap-colorpicker.min.js"));

            // moment
            bundles.Add(new ScriptBundle("~/vendors/moment").Include(
                      "~/Content/vendors/moment/min/moment.min.js"));

            // CSS style (morris.js)
            bundles.Add(new StyleBundle("~/Content/morrisjs").Include(
                      "~/Content/vendors/morris.js/morris.css"));
            // morris.js
            bundles.Add(new ScriptBundle("~/Vendors/morrisjs").Include(
                       "~/Content/vendors/raphael/raphael.min.js",
                       "~/Content/vendors/morris.js/morris.min.js"));

            // normalize-css: see Ion.RangeSlider

            // CSS style (nprogress)
            bundles.Add(new StyleBundle("~/Content/nprogress").Include(
                      "~/Content/vendors/nprogress/nprogress.css"));
            // nprogress
            bundles.Add(new ScriptBundle("~/Vendors/nprogress").Include(
                      "~/Content/vendors/nprogress/nprogress.js"));

            // pdfmake
            bundles.Add(new ScriptBundle("~/vendors/pdfmake").Include(
                      "~/Content/vendors/pdfmake/build/pdfmake.min.js",
                      "~/Content/vendors/pdfmake/build/vfs_fonts.js"));

            // CSS style (PNotify)
            bundles.Add(new StyleBundle("~/Content/pnotify").Include(
                      "~/Content/vendors/pnotify/dist/pnotify.css",
                      "~/Content/vendors/pnotify/dist/pnotify.buttons.css",
                      "~/Content/vendors/pnotify/dist/pnotify.nonblock.css"));
            // PNotify
            bundles.Add(new ScriptBundle("~/Vendors/pnotify").Include(
                      "~/Content/vendors/pnotify/dist/pnotify.js",
                      "~/Content/vendors/pnotify/dist/pnotify.buttons.js",
                      "~/Content/vendors/pnotify/dist/pnotify.nonblock.js"));

            // raphael : see morris.js

            // requirejs : unknown usage

            // CSS style (select2)
            bundles.Add(new StyleBundle("~/Content/select2").Include(
                      "~/Content/vendors/select2/dist/css/select2.min.css"));
            // select2
            bundles.Add(new ScriptBundle("~/Vendors/select2").Include(
                      "~/Content/vendors/select2/dist/js/select2.full.min.js"));

            // skycons
            bundles.Add(new ScriptBundle("~/Vendors/skycons").Include(
                      "~/Content/vendors/skycons/skycons.js"));

            // CSS style (starrr)
            bundles.Add(new StyleBundle("~/Content/starrr").Include(
                      "~/Content/vendors/starrr/dist/starrr.css"));
            // starrr
            bundles.Add(new ScriptBundle("~/Vendors/starrr").Include(
                      "~/Content/vendors/starrr/dist/starrr.js"));

            // CSS style (switchery)
            bundles.Add(new StyleBundle("~/Content/switchery").Include(
                      "~/Content/vendors/switchery/dist/switchery.css"));
            // switchery
            bundles.Add(new ScriptBundle("~/Vendors/switchery").Include(
                      "~/Content/vendors/switchery/dist/switchery.js"));

            // transitionize: used in switchery but cant see how
        }
    }
}

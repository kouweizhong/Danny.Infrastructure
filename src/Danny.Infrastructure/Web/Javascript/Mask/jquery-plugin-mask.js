jQuery.mask=(function($) {
    var ajaxMask = {
        addMask: function () {
            var divLoading = document.getElementById("divmask");
            if (!divLoading) {
                var maskDiv = '<div id="divmask" style="background: #CCC; display: none;"><img id="loading" src="../ajax-loader.gif" /></div>';
                $("body").append(maskDiv);
                var offsettop = 0;
                var offsetleft = 0;
                var width = $(document).width() + "px";
                var height = $(document).height() + "px";
                $("#divmask").css({
                    position: "fixed",
                    'top': offsettop,
                    'left': offsetleft,
                    'width': width,
                    'height': height,
                    'z-index': 99999999,
                    'opacity': '0.4'
                });
                $("#loading").center();
            }
        },
        showMask: function () {
            ajaxMask.addMask();
            $("#loading").center();
            $("#divmask").show();
        },
        hideMask: function () {
            $("#divmask").hide();
        }
    };

    jQuery.fn.center = function () {

        this.css("position", "fixed");

        this.css("top", "45%");

        this.css("left", ($(window).width() - this.width()) / 2 + $(window).scrollLeft() + "px");

        return this;

    };

    return { "showMask": ajaxMask.showMask, "hideMask": ajaxMask.hideMask };

})(jQuery);
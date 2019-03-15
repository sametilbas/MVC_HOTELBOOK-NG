$(document).ready(function () {
    $(".section").not(":first").hide();
    $("ul#menu li:first").addClass("active").show();

    $("ul#menu li").click(function () {
        $("ul#menu li.active").removeClass("active");
        $(this).addClass("active");
        $(".section").slideUp();
        $($('a', this).attr("href")).slideDown('slow');

        return false;
    });

});
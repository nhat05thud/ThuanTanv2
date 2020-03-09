(function ($) {
    $(function () {
        myfunload();
        moreImage();
        cartOpen(".header-right__form--cart .item .item-visible");
        $(".product-colors .item").click(function () {
            $(".product-colors .item").removeClass("active");
            $(this).addClass("active");
        });
        $('[data-toggle="tooltip"]').tooltip();
        
    });
})(jQuery);
$(".ajaxLoginForm").on('submit', function () {
    if (!$(this).find("input").hasClass("input-validation-error") || !$(this).find("textarea").hasClass("input-validation-error")) {
        $(".login__loading--div").css("display", "block");
    }
});
$(".ajaxForm").on('submit', function () {
    if (!$(this).find("input").hasClass("input-validation-error") || !$(this).find("textarea").hasClass("input-validation-error")) {
        $(".loading_div").css("display", "block");
    }
});
$(".quantity-minus").click(function () {
    var oldValue = parseInt($(this).parent().find("input[type='number']").val());
    if (oldValue > 1) {
        $(this).parent().children("input[type='number']").val(oldValue - 1);
    }
    cart.updateTotalPrice($(this).parent().children("input[type='number']"), $(this).parent().children("input[type='number']").val());
});
$(".quantity-plus").click(function () {
    var oldValue = parseInt($(this).parent().find("input[type='number']").val());
    $(this).parent().children("input[type='number']").val(oldValue + 1);
    cart.updateTotalPrice($(this).parent().children("input[type='number']"), $(this).parent().children("input[type='number']").val());
});
$(".product-colors .item").click(function () {
    var itemData = {
        id: $(this).data("id"),
        color: $(this).data("color")
    };
    $("body").append("<div class='loading_div'></div>");
    $(".loading_div").show();
    $.ajax({
        type: "POST",
        data: itemData,
        url: "/admin/surface/product/renderproductitem",
        success: function (data) {
            $(".wrap-product-detail .left").html(data);
            moreImage();
            $(".loading_div").remove();
        }
    });
});
$("#add-to-cart").off("click").on("click", function () { cart.addToCart(this); });
$("#buy-now").off("click").on("click", function () { cart.buy(this); });
$(".cart-body__item--name > span.del-product").click(function () { cart.deleteCartItem(this, $(this).data("id"), $(this).data("color")); });
$(document).on("click", ".remove-item", function () { cart.deleteCartItem(this, $(this).data("id"), $(this).data("color")); });
$(".cart-header button#delete-all-cart").click(function () { cart.deleteAllCart(); });
$(".cart-body__item--quantity input[type='number']").focusout(function () {
    if ($(this).val() <= 0) {
        $(this).val(1);
    }
    cart.updateTotalPrice(this, $(this).val());
});
$(".cart-header button#keep-shopping").click(function() {
    $.ajax({
        type: "POST",
        url: "/admin/surface/cart/keepshopping",
        success: function (data) {
            window.location = data.link;
        }
    });
});
$(".cart-footer button#payment").click(function () {
    $.ajax({
        type: "POST",
        url: "/admin/surface/cart/gotopayment",
        success: function (data) {
            window.location = data.link;
        }
    });
});
$(".wrap-main__aside.filter--product .item ul.color li").click(function() { filter.filterColor(this); });
$(document).click(function (e) {
    var container = $("#header");
    if (!container.is(e.target) && container.has(e.target).length === 0) {
        $("#menu li.li-dropdown").removeClass("a--active");
        $("#menu li.li-dropdown").find(".nav-main__sub").removeClass("nav--active");
        $(".header-right__form--cart .wrap-hidden").removeClass("active");
        $("#mobile-overlay").removeClass("menu-is--active");
    }
}); 
function playVideo(container, src, type) {
    if (type === "youtubeLink") {
        var iframe = '<div id="video--wrap"><iframe src="' + src + '" width="100%" frameborder="0" allow="autoplay; encrypted-media" allowfullscreen></iframe></div>';
        container.html(iframe);
    } else {
        var video = '<video width="100%" controls><source src="' + src + '" type="video/mp4"/></video>';
        container.html(video);
    }
}
$(".product-carousel .item-video").click(function () {
    var src = $(this).attr("data-href");
    var type = $(this).attr("data-type");
    var name = $(this).attr("data-name");
    var container = $("#videoModal .modal-body");
    $("#videoModal h5.modal-title").text(name);
    playVideo(container, src, type);
    $(this).parent().find(".item-video").removeClass("active");
    $(this).addClass("active");
    $("#videoModal").modal("show");
});
$("#videoModal").on("hidden.bs.modal", function (e) {
    $("#videoModal .modal-body").html("");
});
function cartOpen(el) {
    $(el).click(function () {
        $(".header-right__form--cart .wrap-hidden").not($(this).next()).removeClass("active");
        $(this).next().toggleClass("active");
        $("#mobile-overlay").addClass("menu-is--active");
        if (!$(this).next().hasClass("active") && !$(".li-dropdown").hasClass("a--active")) {
            $("#mobile-overlay").removeClass("menu-is--active");
        }
        if ($("#menu li.li-dropdown").hasClass("a--active")) {
            $("#menu li.li-dropdown").removeClass("a--active");
            $("#menu li.li-dropdown").find(".nav-main__sub").removeClass("nav--active");
        }
    });
}
$("#select-capacity").on("change", function () { filter.filterCapacity($(this).val()); });
function myfunload() {
    $(".panel-a").mobilepanel();
    $("#menu > li").not(".home").clone().appendTo($("#menuMobiles"));
    $(".menuTop > ul > li").clone().appendTo($("#menuMobiles"));
    $("#menuMobiles input").remove();
    $("#menuMobiles > li > a").append('<span class="fa fa-chevron-circle-right iconar"></span>');
    $("#menuMobiles li li a").append('<span class="fa fa-angle-right iconl"></span>');
    $("#menu > li:last-child").addClass("last");
    $("#menu > li:first-child").addClass("fisrt");

    $("#menu > li").find("ul").addClass("menu-level");

    $("#menu li.li-dropdown > a").click(function (e) {
        e.preventDefault();
        $(this).parent().toggleClass("a--active");
        $(this).parent().find(".nav-main__sub").toggleClass("nav--active");
        $("#mobile-overlay").addClass("menu-is--active");
        if (!$(this).parent().hasClass("a--active") && !$(".header-right__form--cart .wrap-hidden").hasClass("active")) {
            $("#mobile-overlay").removeClass("menu-is--active");
        }
        $(".header-right__form--cart .wrap-hidden").removeClass("active")
    });

    $(".main-banner").owlCarousel({
        items: 1,
        lazyLoad: true,
        loop: true,
        nav: false,
        dots: false,
        autoplay: true,
        autoplayTimeout: 7000,
        autoplayHoverPause: true
    });

    $(".product-carousel").owlCarousel({
        margin: 30,
        lazyLoad: true,
        loop: false,
        nav: true,
        dots: false,
        autoplay: false,
        autoplayTimeout: 7000,
        autoplayHoverPause: true,
        responsive: {
            0: {
                items: 2
            },
            480: {
                items: 3
            },
            860: {
                items: 4
            },
            1200: {
                items: 4
            }
        }
    });
}
function logout() {
    $("body > .loading_div").css("display", "block");
    $.ajax({
        type: "POST",
        url: "/admin/surface/memberfrontend/logout",
        success: function () {
            $("body > .loading_div").css("display", "none");
            location.reload();
        }
    });
}
function moreImage() {
    $(".more-image").owlCarousel({
        margin: 10,
        lazyLoad: true,
        loop: false,
        nav: false,
        dots: false,
        autoplay: false,
        autoplayTimeout: 7000,
        autoplayHoverPause: true,
        responsive: {
            0: {
                items: 3
            },
            480: {
                items: 3
            },
            600: {
                items: 4
            },
            1000: {
                items: 4
            },
            1200: {
                items: 5
            }
        }
    });
}
function onSuccess() {
    $(".loading_div").css("display", "none");
    $("#divUpdateMessage").removeClass("alert alert-danger").addClass("alert alert-success");
}
function onFailure() {
    $(".loading_div").css("display", "none");
    $("#divUpdateMessage").addClass("alert alert-danger");
}
$(window).scroll(function () {
    if ($(this).scrollTop() > 100) {
        $('.scroll-to-top').fadeIn();
    } else {
        $('.scroll-to-top').fadeOut();
    }
});

$('.scroll-to-top').on('click', function (e) {
    e.preventDefault();
    $('html, body').animate({ scrollTop: 0 }, 800);
    return false;
});
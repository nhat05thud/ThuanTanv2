var cart = {
    properties: {
        carts: [],
        cartsId: [],
        cartWrapHeader: "#wrap-into-cart",
        cartItemHeader: ".items-in-cart .item",

        id: ".product-colors .item.active",
        parents: ".wrap-product-detail",
        image: ".main-image > img",
        title: "a.item-title",
        price: ".current-price > span",
        
        cartImage: ".item-image",
        cartTitle: ".item-title",
        cartPrice: ".current-price",
        cartQuantity: ".item-quantity"
    },
    init: function () {
        cart.checkSessionCart();
        setTimeout(function () { cart.checkItemVisible() }, 500);
    },
    isInArray: function (value, array) {
        return array.indexOf(value) > -1;
    },
    getItemProperties: function (e) {
        var item = {
            id: $(e).parents(cart.properties.parents).find(cart.properties.id).data("id"),
            image: $(e).parents(cart.properties.parents).find(cart.properties.image).attr("src"),
            name: $(e).parents(cart.properties.parents).find(cart.properties.title).text(),
            color: $(e).parents(cart.properties.parents).find(cart.properties.id).data("color"),
            url: $(e).parents(cart.properties.parents).find(cart.properties.title).attr("href"),
            price: $(e).parents(cart.properties.parents).find(cart.properties.price).text() !== "" ? $(e).parents(cart.properties.parents).find(cart.properties.price).text().split(".").join("") : 0,
            quantity: 1
        }
        return item;
    },
    addItemToCart: function (e) {
        var item = cart.getItemProperties(e);
        if (!cart.isInArray(item.id, cart.properties.cartsId)) {
            cart.properties.cartsId.push(item.id);
            cart.properties.carts.push(item);
        } else {
            if (cart.properties.carts.some(cart => cart.id === item.id && cart.color === item.color)) {
                var cartItem = cart.properties.carts.find(cart => cart.id === item.id && cart.color === item.color);
                if (cartItem) {
                    cartItem.quantity = parseInt(cartItem.quantity) + 1;
                }
            } else {
                cart.properties.carts.push(item);
            }
        }
    },
    buy: function (e) {
        $(e).attr("disabled", "disabled");
        cart.addItemToCart(e);
        cart.addSessionCart(cart.properties.carts, "buy");
        setTimeout(function () { $(e).removeAttr("disabled"); }, 500);
    },
    addToCart: function (e) {
        $(e).attr("disabled", "disabled");
        cart.addItemToCart(e);
        cart.genarateCart(cart.properties.carts, "addToCart");
        cart.addSessionCart(cart.properties.carts);
        setTimeout(function () { $(e).removeAttr("disabled"); }, 500);
    },
    addSessionCart: function (list, action) {
        $.ajax({
            type: "POST",
            data: { model: JSON.stringify(list) },
            url: "/admin/surface/cart/addtocart",
            success: function (res) {
                if (action === "buy") {
                    $.ajax({
                        type: "POST",
                        url: "/admin/surface/cart/gotopayment",
                        success: function (data) {
                            window.location = data.link;
                        }
                    });
                } else {
                    toastr.success("Thêm vào giỏ hàng thành công!", "Thành công");
                }
            }
        });
    },
    deleteCartItem: function (item, id, color) {
        $.ajax({
            type: "POST",
            data: { id: parseInt(id), color: color },
            url: "/admin/surface/cart/deletecartitem",
            success: function (res) {
                if ($(cart.properties.cartItemHeader).length > 1) {
                    $(item).parent().remove();
                    var total = parseInt($(cart.properties.cartWrapHeader).children(".total").find("i").text().split(".").join("")) - parseInt(res.price);
                    $(cart.properties.cartWrapHeader).children(".total").find("i").text(cart.formatNumberThousand(total));
                } else {
                    $(cart.properties.cartWrapHeader).html("<div class='empty-cart'><p>Chưa có sản phẩm nào trong giỏ hàng</p></div>");
                }
                cart.updateQuantity();
                var cartItem = cart.properties.carts.filter(function (obj) {
                    return obj.id === id && obj.color === color;
                });
                var cartIndex = cart.properties.carts.findIndex(x => x.id === cartItem[0].id && x.color === cartItem[0].color);
                cart.properties.carts.splice(cartIndex, 1);
                var carts = cart.properties.carts.filter(function (obj) {
                    return obj.id === id;
                });
                if (carts.length === 0) {
                    cart.properties.cartsId = $.grep(cart.properties.cartsId, function (value) {
                        return value !== id;
                    });
                }
                toastr.success("Xóa sản phẩm trong giỏ thành công!", "Thành công");
            }
        });
    },
    deleteAllCart: function () {
        $.ajax({
            type: "POST",
            url: "/admin/surface/cart/deleteallcart",
            success: function (data) {
                $("#wrapper-cart").html(data);
                cart.checkSessionCart();
                cart.properties.cartsId = [];
                cart.properties.carts = [];
            }
        });
    },
    genarateCart: function (list) {
        $.ajax({
            type: "POST",
            data: { model: JSON.stringify(list) },
            url: "/admin/surface/cart/rendercartitem",
            success: function (res) {
                $(cart.properties.cartWrapHeader).html(res);
                var quantity = 0;
                for (var i in list) {
                    quantity += parseInt(list[i].quantity);
                }
                $(".cart-form .item-visible > span").text(quantity);
            }
        });
    },
    checkSessionCart: function () {
        $.ajax({
            type: "POST",
            url: "/admin/surface/cart/initcart",
            success: function (res) {
                $(cart.properties.cartWrapHeader).html(res);
                cart.updateQuantity();
            }
        });
    },
    checkItemVisible: function() {
        $(cart.properties.cartItemHeader).each(function (i, e) {
            var item = {
                id: $(e).find(cart.properties.cartTitle).data("id"),
                image: $(e).find(cart.properties.cartImage).attr("src"),
                name: $(e).find(cart.properties.cartTitle).text(),
                url: $(e).find(cart.properties.cartTitle).attr("href"),
                price: $(e).find(".price").children("span").first().text() !== "" ? $(e).find(".price").children("span").first().text().split(".").join("") : 0,
                color: $(e).find(".color").children("span").text(),
                quantity: $(e).find(".price").find(cart.properties.cartQuantity).text()
            }
            cart.properties.cartsId.push(item.id);
            cart.properties.carts.push(item);
        });
    },
    updateQuantity: function () {
        var quantity = 0;
        $(cart.properties.cartItemHeader).each(function (i, e) {
            quantity += parseInt($(e).find(".item-quantity").text());
        });
        $(".cart-form .item-visible > span").text(quantity);
    },
    updateTotalPrice: function (e, quantity) {
        $(".loading_div").css("display", "block");
        $.ajax({
            type: "POST",
            data: { id: parseInt($(e).data("id")), quantity: parseInt(quantity) },
            url: "/admin/surface/cart/updatetotalprice",
            success: function (res) {
                $(".cart-footer ul li .currency > span").text(cart.formatNumberThousand(res.total));
                cart.checkSessionCart();
                $(".loading_div").css("display", "none");
            }
        });
    },
    formatNumberThousand: function(num) {
        return num.toString().replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1.");
    }
}
cart.init();
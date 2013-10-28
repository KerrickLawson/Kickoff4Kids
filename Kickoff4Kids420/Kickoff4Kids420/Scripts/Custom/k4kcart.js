$(document).ready(function () {   
    var shoppingCart = function () {
        var removeLink = $(".RemoveLink"), updateMessage = $('#update-message'),
            cartTotal = $('#cart-total'), cartStatus = $('#cart-status'),
            studentPointTotal = parseInt($('#studentPointTotal').text()), purchaseBtn = $('#purchaseBtn');
        
        init = function() {
            if (parseInt(cartTotal.text()) > studentPointTotal || studentPointTotal === 0) {
                purchaseBtn.hide();
                $('#warning-message').text('You do not currently have the funds to make this purchase.');
                if (parseInt(cartTotal.text()) === 0){}
            }
            if (parseInt(cartTotal.text()) === 0) {
                purchaseBtn.hide();
            }
        };
        removeLink.click(function () {
            var recordToDelete = $(this).attr("data-id");
            if (recordToDelete != '') {
                $.post("/ShoppingCart/RemoveFromCart", { "id": recordToDelete },
                    function (data) {
                        if (data.ItemCount == 0) {
                            $('#row-' + data.DeleteId).fadeOut('slow');
                        } else {
                            $('#item-count-' + data.DeleteId).text(data.ItemCount);
                        }
                        cartTotal.text(data.CartTotal);
                        updateMessage.text(data.Message);
                        cartStatus.text('Cart (' + data.CartCount + ')');
                        if (parseInt(cartTotal.text()) > studentPointTotal) {
                            purchaseBtn.fadeOut();
                            $('#warning-message').fadeIn();
                        } else {
                            purchaseBtn.fadeIn();
                            $('#warning-message').fadeOut();
                        }
                        if (parseInt(cartTotal.text()) === 0) {
                            purchaseBtn.fadeOut();
                            $('#warning-message').fadeOut();                           
                        }
                    });
            }
        });
        return { init: init };
    }();
    shoppingCart.init();
})

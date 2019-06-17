// Write your JavaScript code.
$(document).ready(function () {
    /*Filters productoverview frontend*/
    $("#searchString").on("keyup",
        function () {
            var value = $(this).val().toLowerCase();
            $("#productlist div").filter(function () {
                $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1);
            });
        });


    /*Filters backend productcategories*/
    $("#productfilter").change(function () {
        var filterValue = $(this).val();
        var row = $(".single-product");

        if (filterValue == "AllCategories") {
            row.show();
        }
        else if (filterValue == "Offer") {
            console.log("test");
            row.hide();
            row.each(function (i, el) {
                var category = $(el).find("input.hasOffer");
                category.each(function (i, el) {
                    console.log("found one");
                    if (el.value == filterValue) {
                        console.log("found more");
                        $(el).closest(row).show();
                    }
                });
            });
        }
        else {
            row.hide();
            row.each(function (i, el) {
                var category = $(el).find("input.productcategory");
                category.each(function (i, el) {
                    if (el.value == filterValue) {
                        $(el).closest(row).show();
                    }
                });
            });
        }
    });

    /*Filters backend roles*/
    $("#userfilters").change(function () {
        var filterValue = $(this).val();
        console.log(filterValue);
        var row = $(".single-user");

        if (filterValue == "AllRoles") {
            row.show();
        } else {
            row.hide();
            row.each(function (i, el) {
                var category = $(el).find("input.userrole");
                category.each(function (i, el) {
                    if (el.value == filterValue) {
                        console.log(el.value);
                        $(el).closest(row).show();
                    }
                });
            });
        }
    });


    /*Filters backend orders*/
    var $divs = $("div.single-user-order");

    $("#orderfilters").change(function () {
        var filterValue = $(this).val();
        console.log(filterValue);
        if (filterValue != "Num") {
            var alphabeticallyOrderedDivs = $divs.sort(function (a, b) {
                return $(a).find("p.sort-email").text() > $(b).find("p.sort-email").text();
            });
            $(".listofusers").html(alphabeticallyOrderedDivs);

        }
      
    });

    $('#orderfilters').change(function () {
        var filterValue = $(this).val();
        console.log(filterValue);
        if (filterValue != "Alpha") {
            var numericallyOrderedDivs = $divs.sort(function (a, b) {
                return $(a).find("p.sort-id").text() - $(b).find("p.sort-id").text();
            });
            $(".listofusers").html(numericallyOrderedDivs);
        }

    });


    /*Checkbox filters*/

    $("#filtercategories :checkbox").click(function () {

        if ($("#filtercategories :checkbox:checked").length > 0) {
            $("div.product").hide();

            $("#filtercategories :checkbox:checked").each(function () {

                if ("div:contains(" + this.value + ")") {
                    $("div:contains(" + this.value + ")").show();
                }
            });
        } else {
            $("div.product").show();
        }
    });


    /*Jquery popups*/

    $(function () {
        $(".dialog").dialog({
            autoOpen: false,
            show: {
                effect: "blind",
                duration: 500
            },
            hide: {
                effect: "fade",
                duration: 500
            }
        });


        $("a.button.add").click(function () {
            var nth = $(".productlist a.button.add").index($(this));
            $("div.dialog"+"."+ nth).dialog('open');
        });
    });



    /* Delete from cart */

    $("p.removefromcart").click(function () {
   
        var nth = $("p.removefromcart").index($(this));
        $(".item-"+nth).dialog({
            resizable: false,
            show: {
                effect: "blind",
                duration: 500
            },
            height: "auto",
            width: 400,
            modal: false,
        });
    });


    //Parallax JS
    var parallaxNode1 = document.getElementsByClassName('parallax1');
    window.addEventListener('scroll', function (e) {
        let speed = parallaxNode1[0].getAttribute('data-speed');
        parallaxNode1[0].style.backgroundPosition = '0px ' + (window.scrollY * speed) + 'px';
    });

    //Textarea

  


});

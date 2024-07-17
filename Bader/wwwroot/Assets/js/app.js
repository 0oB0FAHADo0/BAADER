


$(document).ready(function () {



    ///////////////// bootstrap Tooltip
    $('[data-bs-toggle="tooltip"]').tooltip();

   
    ///////////////// Side menu
    //Open and Close Menu
    $('#menu_state_btn').on('click', function(){
        let isOpened = $('body').hasClass('menu_state_open')
        isOpened ? $('body').removeClass('menu_state_open') : $('body').addClass('menu_state_open')
    })


    // menu active link
    let currentPage = window.location.pathname;
    $('.menu_links a').each(function () {
        let hrefName = $(this).attr('href');
        if (('/' + hrefName) === currentPage) {
            $('.menu_links a').removeClass('active');
            $(this).addClass('active');
        }
    });

    // menu open on hover
    let menu_links_height = $('.menu_links').outerHeight(true);

    $('.sub_menu > ul').each(function () {
        let ul_hight = Math.round($(this).outerHeight(true));
        $(this).attr('hidden-hight', ul_hight);

        let hasActiveClass = $(this).find('a').hasClass('active');
        hasActiveClass ? ($(this).css("height", ul_hight), $(this).parents('.sub_menu').addClass('open')) : $(this).css("height", "0px");
    });

    // $('.sub_menu').hover(function () {

    //     let $el = $(this).find("ul"),
    //         $el_height = Math.round($el.attr('hidden-hight'))
    //         $el_offset_top = Math.round($el.offset().top);

    //     $el.css("height", $el_height);
    //     $el.parents('.sub_menu').addClass('open');

    //     // let btm_position = $el_offset_top + $el_height;
    //     // if (btm_position >= menu_links_height) {

    //     //     $('.menu_links').scrollTop(btm_position);
    //     //     // $el.css({
    //     //     //     "inset": "auto auto 0px 155px"
    //     //     // });
    //     // }

    // }, function () {        
    //     if (!$(this).find('a').hasClass('active')) {
    //         $(this).find("ul").css("height", "0px");
    //         $('.sub_menu').removeClass('open');
    //     }
    // });


    $('.sub_menu > a').on('click', function () {
        if ($(this).parents('.sub_menu').hasClass('open')) {
            $(this).parents('.sub_menu').find("ul").css("height", "0px");
            $('.sub_menu').removeClass('open');
        } else {
            let $el = $(this).parents('.sub_menu').find("ul"),
                $el_height = Math.round($el.attr('hidden-hight'))
            $el_offset_top = Math.round($el.offset().top);

            $('.sub_menu').find("ul").css("height", "0px");
            $('.sub_menu').removeClass('open');

            $el.css("height", $el_height);
            $el.parents('.sub_menu').addClass('open');
        }
    });

    ///////////////// stop closing dropdown on click
    $(".dropdown-menu").click(function (e) {
        e.stopPropagation();
    });

    ///////////////// add active class to header nav
    $('section .nav .nav-link').on('click', function () {
        $('section .nav .nav-link').removeClass('active');
        $(this).addClass('active');
    });

    ///////////////// datatable search input
    $('.table__quick_search').quicksearch('table tbody tr');

    



});

/////////////// DATA TABLE Customization /////////////// 
$(document).ready(function () {
    let normal_table = $(".js-dataTable").DataTable({
        scroller: true,
        dom: '<"table_wrapper"t><"bottom_bar"ip><"clear">',
        language: {
            url: "Assets/lib/DataTable/js/dataTable_ar.json",
        },
    });
});



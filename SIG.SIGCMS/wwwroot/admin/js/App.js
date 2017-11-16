﻿$(function () {

    var resetwidth = function () {

        var winheight = $(window).height(), winwidth = $(window).width();

        var height = Math.max($("#sidebar-nav").height(), $("#rightcol").height(), winheight);
        //  $("#sidebar-nav").height(height);
        //  $("#rightcol").height(height);

        $('#rightcol,#sidebar-nav').css({ 'min-height': height + "px" })
        if (winwidth <= 768) {
            $('#rightcol').css({ 'width': winwidth + "px" })
        } else {
            $('#rightcol').css({ 'width': "auto" })
        }
    }

    resetwidth();

    $(window).resize(function () {
        resetwidth();
    });

    var pid = $('.mainmenu a.active').closest('li').attr("data-parent");
    $('.mainmenu li[data-parent=' + pid + ']').fadeIn();

    //查子项数量
    $.each($(".down-nav>a"), function (index, value) {
        var id = $(value).attr("data-id");
        var licount = $('.mainmenu li[data-parent=' + id + ']').length;
        var html = '<span class="badge badge-pill badge-secondary">' + licount + '</span>';
        $(value).append(html);
    });

    $(".down-nav>a").click(function (e) {
        e.preventDefault();

        $(this).next(".submenu").slideToggle();
        var li = $(this).closest('li');
        li.toggleClass('nav-open');

    });


    $('.closemenu a').on('click', function (e) {

        //  $('#rightcol').css({ 'margin-left': '0' });
        closenav();
        e.preventDefault();
    });

    $('.showmenu').on('click', function (e) {

        var marginLeft = $('#rightcol').css("margin-left");
        // console.log($('#rightcol').css("margin-left"));

        if (marginLeft === '0px') {
            $('#rightcol').animate({ 'marginLeft': '170' }, 'fast');
            $('#wrapper').removeClass('nonav');

        } else {
            closenav();
        }

        e.preventDefault();
    });

    var closenav = function () {
        $('#rightcol').animate({ 'marginLeft': '0' }, 'fast');
        $('#wrapper').addClass("nonav");
    }



    $('a.expand').click(function (e) {
        $(this).closest('.box').addClass('box-fixed');
        $(this).hide();
        $(this).next('a').show()
        e.preventDefault();
    });
    $('a.compress').click(function (e) {
        $(this).closest('.box').removeClass('box-fixed');
        $(this).hide();
        $(this).prev('a').show()
        e.preventDefault();
    });
});



var Common = {
    //消息提示
    ShowBox: function (status, message, title) {
        switch (status) {
            case 1:
                toastr.success(message, title)
                break;
            case 2:
                toastr.error(message, title)
                break;
            case 3:
                toastr.info(message, title)
                break;
            case 4:
                toastr.warning(message, title)
        }
    },
    ShowBoxWithFunc: function (data, title, func) {

        switch (data.status) {
            case 1:
                toastr.success(data.message, title)
                func();
                break;
            case 2:
                toastr.error(data.message, title)
                break;
            case 3:
                toastr.info(data.message, title)
                break;
            case 4:
                toastr.warning(data.message, title)
        }
    },
    ShowBoxWithFuncBack: function (data, title, func) {

        switch (data.status) {
            case 1:
                toastr.success(data.message, title);
                func(data.id, data.data);
                break;
            case 2:
                toastr.error(data.message, title);
                break;
            case 3:
                toastr.info(data.message, title);
                break;
            case 4:
                toastr.warning(data.message, title);
        }
    },
    SubmitBack: function (data, title, container) {

        switch (data.status) {
            case 1:
                toastr.success(data.message, title)
                if (container !== undefined)
                    container.html(data.Data)
                break;
            case 2:
                toastr.error(data.message, title)
                break;
            case 3:
                toastr.info(data.message, title)
                break;
            case 4:
                toastr.warning(data.message, title)
        }
    },
    PageSizeSet: function (url, title, pageSize, func) {   //分页设置

        $.post(url, { pageSize: pageSize }, function (data) {

            switch (data.status) {
                case 1:
                    //toastr.success(data.message, title);
                    func();
                    break;
                case 2:
                    toastr.error(data.message, title);
                    break;
                case 3:
                    toastr.info(data.message, title);
                    break;
                case 4:
                    toastr.warning(data.message, title);
                    break;
            }
        });
    },

    SingleActionWithFunc: function (url, title, that, func) {   //真假值修改操作

        $.post(url, $("#anti-form").serialize(), function (data) {

            switch (data.status) {
                case 1:
                    toastr.success(data.message, title);
                    func(that);
                    break;
                case 2:
                    toastr.error(data.message, title);
                    break;
                case 3:
                    toastr.info(data.message, title);
                    break;
                case 4:
                    toastr.warning(data.message, title);
                    break;
            }
        });
    },
    SingleActionWithFuncBack: function (url, title, that, func) {   //真假值修改操作

        $.post(url, $("#anti-form").serialize(), function (data) {

            switch (data.status) {
                case 1:
                    toastr.success(data.message, title);
                    func(that, data.Data);
                    break;
                case 2:
                    toastr.error(data.message, title);
                    break;
                case 3:
                    toastr.info(data.message, title);
                    break;
                case 4:
                    toastr.warning(data.message, title);
                    break;
            }
        });
    },

    SingleAction: function (url, title, isTips) {   //真假值修改操作
        $.post(url, $("#anti-form").serialize(), function (data) {
            if (!isTips)
                return;

            switch (data.status) {
                case 1:
                    toastr.success(data.message, title);
                    setTimeout(function () {
                        location.reload();
                    }, 1000);
                    break;
                case 2:
                    toastr.error(data.message, title);
                    break;
                case 3:
                    toastr.info(data.message, title);
                    break;
                case 4:
                    toastr.warning(data.message, title);
                    break;
            }
        });
    },



    DeleteItem: function (url, title, that) {   //删除

        $.post(url, $("#anti-form").serialize(), function (data) {
            switch (data.status) {
                case 1:
                    toastr.success(data.message, title);
                    that.closest('.item-container').remove();
                    break;
                case 2:
                    toastr.error(data.message, title);
                    break;
                case 3:
                    toastr.info(data.message, title);
                    break;
                case 4:
                    toastr.warning(data.message, title);
                    break;
            }
        });
    }



};

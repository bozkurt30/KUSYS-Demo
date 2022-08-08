// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

showInPopup = (url, title,btnName,btnColor,bgColor,hidden) => {
    $.ajax({
        type: 'GET',
        url: url,
        success: function (res) {
            $('#form-modal .modal-body').html(res);
            $('#form-modal .modal-title').html(title);
            $('#frmBtn .btnName').html(btnName);
            $('#frmBtn .btnName').addClass(btnColor);
            if (hidden == "hidden") {
                $(".validUsername").hide();
            }
            $('.modal-header').css('background-color', bgColor);
            $('#form-modal').modal('show');
            // to make popup draggable
            $('.modal-dialog').draggable({
                handle: ".modal-header"
            });
        }
    })
}

jQueryAjaxPost = form => {
    try {
        $.ajax({
            type: 'POST',
            url: form.action,
            data: new FormData(form),
            contentType: false,
            processData: false,
            success: function (res) {
                //if (res.resultSelect =="courseSelect") {
                //    $.notify("Kurs seçme başarılı bir şekilde gerçekleşti!", "success");
                //} else {
                //    $.notify("Kurs seçerken hata oldu!", "error");
                //}
                if (!res.result) {
                    if (res.isValid) {
                        $.notify("İşlem başarılı bir şekilde gerçekleşti!", "success");
                        $('#view-all').html(res.html)
                        setTimeout(function () {
                            window.location.reload(1);
                        }, 500);
                        $('#form-modal').modal('hide');
                        $('#form-modal .modal-body').html('');
                        $('#form-modal .modal-title').html('');
                        $('#frmBtn .btnName').html('');
                        if (hidden=="hidden") {
                            $(".validUsername").hide();
                        }
                        $('#frmBtn .btnName').addClass(btnColor);
                        $('.modal-header').css('background-color', bgColor);

                    }
                    else {
                        $.notify("Lütfen Boş Alanları Doldurunuz!", "error");
                        /*$('#form-modal .modal-body').html(res.html);*/
                        //$('#frmBtn .btnName').addClass(btnColor);
                        //$('.modal-header').css('background-color', bgColor);
                    }
                } else {
                    $.notify("Lütfen Başka Bir Kullanıcı Adı Yazınız!", "error");
                }
            },
            error: function (err) {
                console.log(err)
            }
        })
        //to prevent default form submit event
        return false;
    } catch (ex) {
        console.log(ex)
    }
}

jQueryAjaxDelete = form => {
    if (confirm('Silmek istediğinizden emin misiniz ?')) {
        try {
            $.ajax({
                type: 'POST',
                url: form.action,
                data: new FormData(form),
                contentType: false,
                processData: false,
                success: function (res) {
                    $('#view-all').html(res.html);
                    $.notify("Silme İşlemi Başarılı.", { globalPosition: 'top right', className: 'success' });
                },
                error: function (err) {
                    $.notify("Silme İşleminde Hata Oluştu!", "error");
                    console.log(err)
                }
            })
        } catch (ex) {
            console.log(ex)
        }
    }

    //prevent default form submit event
    return false;
}

jQueryAjaxSelectCoursePost = form => {
    try {
        $.ajax({
            type: 'POST',
            url: form.action,
            data: new FormData(form),
            contentType: false,
            processData: false,
            success: function (res) {
                if (res.sonuc) {
                    $.notify("Bu Kursu Zaten Seçtiniz! Başka kurs seçiniz", "warn");
                } else {
                    if (res.resultSelect) {
                        $.notify("Kurs seçme başarılı bir şekilde gerçekleşti!", "success");
                    } else {
                        $.notify("Kurs seçerken hata oldu!", "error");
                    }
                }
            },
            error: function (err) {
                console.log(err)
            }
        })
        //to prevent default form submit event
        return false;
    } catch (ex) {
        console.log(ex)
    }
}

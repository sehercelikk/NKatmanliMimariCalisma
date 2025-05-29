$(document).ready(function () {
    //Swal.fire({ title: "Good job!", text: "You clicked the button!", icon: "success" });
})

function AjaxAddUpdate(postURL, formData, triggerName, closeModalButton, triggerButton) {
    console.log(formData);
    Swal.fire({
        title: "",
        text: "Lütfen Bekleyin",
        onOpen: function () {
            Swal.showLoading();
            $.ajax({
                type: "POST",
                url: postURL,
                data: formData,
                dataType: "json",
                processData: false,
                contentType: false,
                timeout: 1800000
            }).done(function (data, status, jqXHR) {
                console.log("DONE : ", status);
                MesajVer(data, closeModalButton);
                Swal.hideLoading();
                $(triggerName).trigger('click');
                $(triggerButton).trigger('click');
            }).fail(function (data, status, jqXHR) {
                console.log(data);
                Swal.hideLoading();
            }).always(function (data, status, jqXHR) {
                MesajVer(data, closeModalButton);
                Swal.hideLoading();

            })
        }
    }).then(function (result) {
        Swal.hideLoading();
    })
}
function AjaxDelete(postURL, formData, triggerName, closeModalButton) {
    Swal.fire({
        title: "",
        text: "Lütfen Bekleyin",
        onOpen: function () {
            Swal.showLoading();
            $.ajax({
                type: "POST",
                url: postURL,
                data: formData,
                dataType: "json",
                processData: false,
                contentType: false
            }).done(function (data, status, jqXHR) {
                MesajVer(data, closeModalButton);
                Swal.hideLoading();
                $(triggerName).trigger('click');
            }).fail(function (data, status, jqXHR) {
                Swal.hideLoading();
            }).always(function (data, status, jqXHR) {
                MesajVer(data, closeModalButton);
                Swal.hideLoading();
            })
        }
    }).then(function (result) {
        Swal.hideLoading();
    })
}
function MesajVer(rt, closeModalButton) {
    if (!rt) {
        Swal.fire({
            icon: "error",
            title: "Hata",
            html: "Hata",
            allowOutsideClick: false
        })
    } else {
        if (rt.resultStatus == 3) {
            Swal.fire({
                icon: "error",
                title: "Hata",
                html: rt.mesaj,
                allowOutsideClick: false
            });
        } else if (rt.resultStatus == 2) {
            Swal.fire({
                icon: "info",
                title: "Bilgi",
                html: rt.mesaj,
                allowOutsideClick: false
            });
        }
        else if (rt.resultStatus == 1) {
            Swal.fire({
                icon: "success",
                title: "Başarılı",
                html: rt.mesaj,
                allowOutsideClick: false
            }).then(function () {
                ModalKapat(closeModalButton);
            });
        }
    }
}
async function GetFunction(url) {
    return $.ajax({
        type: 'GET',
        url: url,
    });
}
function ModalKapat(triggerName) {
    $(triggerName).trigger('click');
}
function ModalTextTemizle(modalName) {
    $(modalName + ' input').val("");
    $(modalName + ' textarea').val("");
    $(modalName + ' select option[value="0"]').prop("selected", true);
}


function CreateNavigator(listElement) {
    return `<li class="breadcrumb-item">${listElement}</li>`;
}



function ZorunluAlanKontrol(selector) {
    let valid = true;

    $(`${selector} .required`).each(function () {
        const val = $(this).val()?.trim();
        const label = $(this).data('label') || $(this).attr('placeholder') || $(this).attr('id');

        if (!val) {
            $(this)
                .addClass('is-invalid')
                .attr('data-bs-toggle', 'tooltip')
                .attr('data-bs-placement', 'top')
                .attr('title', `${label} alanı boş olamaz.`)
                .tooltip('dispose') // önceki tooltipi temizle
                .tooltip('show');   // yeni tooltipi göster

            valid = false;
        } else {
            $(this)
                .removeClass('is-invalid')
                .removeAttr('data-bs-toggle')
                .removeAttr('data-bs-placement')
                .removeAttr('title')
                .tooltip('dispose'); // varsa eski tooltipi temizle
        }
    });

    return { valid };
}



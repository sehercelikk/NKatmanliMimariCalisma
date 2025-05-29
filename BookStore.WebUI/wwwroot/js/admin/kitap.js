$(document).ready(function () {
    GetKitaplar('.data-area');
    GetYazarlar('#yazarDropdown');
})

$(document).on('click', '#btnGetHidden', function () {
    GetKitaplar('.data-area');
})


$(document).on('click', '.btnUpdate', async function () {
    $('#addUpdateModalLabel').html("Kitap Güncelle");
    var id = $(this).data("id");
    var result = await GetFunction(`KitapGuncelle/${id}`);
    $('#kitapId').val(id);
    $('#KitapAdi').val(result.kitapAdi);
    $('#yazarId').val(result.yazarId);
    $('#yazarDropdown').val(result.yazarId);

    //$('.status').show();
    //if (result.aktifMi == 1)
    //    $('#AktifMi').prop('checked', true);
    //else
    //    $('#AktifMi').prop('checked', false);
})


function GetYazarlar(element) {
    $.ajax({
        url: 'GetYazarlar',
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            var dropdown = $(element);

            $.each(data, function (index, item) {
                dropdown.append($('<option>', {
                    value: item.id,
                    text: item.yazarAdi
                }));
            });
        },
        error: function () {
            alert('Yazarlar yüklenirken hata oluştu.');
        }
    });
}





$(document).on('click', '.btnAddModal', function () {
    $('#addUpdateModalLabel').html("Kitap Ekle");
    $('.status').hide();

    $('#kitapId').val('');
    $('#KitapAdi').val('');
    $('#yazarDropdown').val('').change();
    $('#AktifMi').prop('checked', false);
    $('#uyariAlani').html('').removeClass().hide();
})


$('#addUpdateModal').on('hidden.bs.modal', function () {
    // İçindeki tüm required alanları temizle
    $(this).find('.required').each(function () {
        $(this)
            .removeClass('is-invalid')
            .removeAttr('title')
            .removeAttr('data-bs-toggle')
            .removeAttr('data-bs-placement')
            .tooltip('dispose');
    });

    // Ayrıca formu da sıfırlamak istersen:
    $(this).find('form')[0]?.reset();

    // Uyarı mesajı alanı varsa onu da temizle
    $('#uyariAlani').html('').removeClass().hide();
});


$(document).on('click', '#btnExecuteAction', function () {

    const { valid, hataMesaji } = ZorunluAlanKontrol('#addUpdateModal');

    if (!valid) {
        $('#uyariAlani').html(hataMesaji).addClass('alert alert-danger').show();
        return;
    } else {
        $('#uyariAlani').hide().html("").removeClass();
    }


    var baslik = "";
    if ($('#kitapId').val() == "" || $('#kitapId').val() == null)
        baslik = "Kitap Ekleme İşlemi";
    else
        baslik = "Kitap Güncelleme İşlemi";
    Swal.fire({
        title: baslik,
        text: "İşlemi Başlatmak İstediğinizden Emin misiniz?",
        icon: "question",
        showCancelButton: !0,
        confirmButtonText: "Başlat",
        cancelButtonText: "İptal",
        confirmButtonClass: "btn btn-success mt-2",
        cancelButtonClass: "btn btn-danger ms-2 mt-2",
        buttonsStyling: !1,
        showLoaderOnConfirm: true,
        allowOutsideClick: false,
    }).then((result) => {
        if (result.isConfirmed) {
            var formData = new FormData();
            formData.append("Id", $('#kitapId').val());
            formData.append("KitapAdi", $('#KitapAdi').val());
            formData.append("YazarId", $('#yazarDropdown').val());

            formData.append("AktifMi", $('#AktifMi').is(':checked'));
            if (!$('#kitapId').val())
                AjaxAddUpdate("KitapEkle", formData, '#btnGetHidden', '.btn-close');
            else
                AjaxAddUpdate("KitapGuncelle", formData, '#btnGetHidden', '.btn-close');
        }
    });
})


$(document).on('click', '.btnDelete', function () {
    Swal.fire({
        title: "Silme İşlemi",
        text: "İşlemi Başlatmak İstediğinizden Emin misiniz?",
        icon: "question",
        showCancelButton: !0,
        confirmButtonText: "Başlat",
        cancelButtonText: "İptal",
        confirmButtonClass: "btn btn-success mt-2",
        cancelButtonClass: "btn btn-danger ms-2 mt-2",
        buttonsStyling: !1,
        showLoaderOnConfirm: true,
        allowOutsideClick: false,
    }).then((result) => {
        if (result.isConfirmed) {
            var formData = new FormData();
            formData.append("id", $(this).attr("data-id"));
            AjaxDelete("KitapSil", formData, '#btnGetHidden', '');
        }
    });
})
function GetKitaplar(element) {
    $.ajax({
        type: 'GET',
        url: "GetKitaplar",
        success: function (data) {
            console.log(data);

            $(element).html("");
            var txt = "";
            txt += `<div class="table-responsive">`;
            txt += `    <div class="card">`;
            txt += `        <div class="card-body">`;
            txt += `            <div class="row">`;
            txt += `                <table class="table table-sm">`;
            txt += `                    <thead>`;
            txt += `                        <tr>`;
            txt += `                            <th scope="col" width="10%" style="border-top: none !important;text-align:center">Sıra No</th>`;
            txt += `                            <th scope="col" width="40%" style="border-top: none !important;">Kitabın Adı</th>`;
            txt += `                            <th scope="col" width="30%" style="border-top: none !important;">Yazarı</th>`;
            txt += `                            <th class="text-end px-4" scope="col" width="15%"  style="border-top:none !important;">İşlem</th>`;
            txt += `                            <th scope="col" width="5%" style="border-top: none !important;"></th>`;
            txt += `                         </tr>`;
            txt += `                     </thead>`;
            $.each(data, function (index, value) {
                txt += `                <tbody>`;
                txt += `                <tr>`;
                txt += `                        <td scope="row" class="text-center">${index + 1} </td>`;
                txt += `                        <td>${value.kitapAdi}</td>`;
                txt += `                        <td>${value.yazarAdi}</td>`;
                txt += `                        <td class="text-end">`;
                txt += `                            <button class="btn btn-one btn-sm bg-info btnUpdate" data-id="${value.id}" title="Güncelle" data-bs-toggle="modal" data-bs-target="#addUpdateModal"><i class="fas fa-pen"></i></button>`;
                txt += `                            <button class="btn btn-one btn-sm bg-danger btnDelete" data-id="${value.id}" title="Sil"><i class="fas fa-trash"></i></button>`;
                txt += `                        </td>`;
                txt += `                     </tr>`;
                txt += `                </tbody>`;
                txt += `            </div>`;
                txt += `        </div>`;
                txt += `    </div>`;
                txt += `</div>`;
            })
            $(element).append(txt);
        }
    });
}


$(document).on('hidden.bs.modal', '#addUpdateModal', function () {
    $('#addUpdateModalLabel').html("");
    ModalTextTemizle('#addUpdateModal');
})


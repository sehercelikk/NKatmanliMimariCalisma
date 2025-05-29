$(document).ready(function () {
    GetKitapIcerikler('.data-area');
})

$(document).on('click', '#btnGetHidden', function () {
    GetKitapIcerikler('.data-area');
})





$(document).on('click', '.btnUpdate', async function () {
    $('#addUpdateModalLabel').html("İçerik Güncelle");
    var id = $(this).data("id");
    var result = await GetFunction(`KitapIcerikGuncelle/${id}`);
    console.log(result);
    $('#kitapIcerikId').val(id);
    $('#yazarDropdown').val(result.data.yazarId).change();
    setTimeout(function () { $('#kitapDropdown').val(result.data.kitapId); }, 500)
    $('#YazarAdi').val(result.yazarAdi);
    $('#KitapAdi').val(result.kitapAdi);
    $('.status').show();
    if (result.aktifMi == 1)
        $('#AktifMi').prop('checked', true);
    else
        $('#AktifMi').prop('checked', false);

})



$(document).on('click', '.btnAddModal', function () {
    $('#addUpdateModalLabel').html("İçerik Ekle");
    $('.status').hide();
})

var files;
$(document).on('change', '#Dosya', function (e) {
    files = e.target.files;
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


    let dosya = files;
    var baslik = "";
    if ($('#sinifDersIcerikId').val() == "" || $('#kitapIcerikId').val() == null)
        baslik = "İçerik Ekleme İşlemi";
    else
        baslik = "İçerik Güncelleme İşlemi";
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
            var durum = 0;
            if ($('#AktifMi').is(':checked'))
                durum = 1;
            var formData = new FormData();
            for (var i = 0; i < dosya.length; i++) {
                formData.append('File', dosya[i]);
            }
            formData.append("Id", $('#kitapIcerikId').val());
            formData.append("YazarId", $('#yazarDropdown').val());
            formData.append("KitapId", $('#kitapDropdown').val());
            formData.append("Dosya", $('#Dosya').val());
            formData.append("AktifMi", durum);
            if (!$('#kitapIcerikId').val())
                AjaxAddUpdate("KitapIcerikEkle", formData, '#btnGetHidden', '.btn-close');
            else
                AjaxAddUpdate("KitapIcerikGuncelle", formData, '#btnGetHidden', '.btn-close');

        }
    });
})

$(document).ready(function () {
    $.ajax({
        url: 'GetYazarlar',
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            var dropdown = $('#yazarDropdown');

            $.each(data, function (index, item) {
                dropdown.append($('<option>', {
                    value: item.id,
                    text: item.yazarAdi
                }));
            });
        },
        error: function () {
            alert('Yazar bilgisi yüklenirken hata oluştu.');
        }
    });
});



$(document).on('change', '#yazarDropdown', function () {
    var selectedYazarId = $(this).val();

    if (selectedYazarId) {
        // Yazara bağlı kitapları almak için Ajax isteği gönderme
        $.ajax({
            type: 'GET',
            url: `GetKitaplarDrop/${selectedYazarId}`, // Backend'deki URL
            success: function (data) {
                // Gelen veriyi kullanarak kitapları dropdown içinde listeleme
                var kitapOptions = '<option value="0">Kitap Seçiniz</option>'; // Varsayılan boş seçenek
                $.each(data, function (index, kitap) {
                    kitapOptions += `<option value="${kitap.id}">${kitap.kitapAdi}</option>`;
                });
                $('#kitapDropdown').html(kitapOptions); // kitap seçimi dropdown'ı
            },
            error: function (xhr, status, error) {
                console.error("Kitaplar alınamadı:", error);
            }
        });
    }
});


//$(document).on('change', ".aktifMi", function () {
//    var durum = 0;
//    if ($(this).is(':checked'))
//        durum = 1;
//    var formData = new FormData();
//    formData.append("id", $(this).data("id"));
//    formData.append("AktifMi", durum);
//    AjaxAddUpdate("KitapIcerikDurumGuncelle", formData, '#btnGetHidden', '.btn-close');
//})




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
            AjaxDelete("KitapIcerikSil", formData, '#btnGetHidden', '');
        }
    });
})


function GetKitapIcerikler(element) {
    $.ajax({
        type: 'GET',
        url: "GetKitapIcerikler/" + $('.btnAddModal').data("id"),
        success: function (data) {
            $(element).html("");
            var txt = "";
            txt += `<div class="table-responsive">`;
            txt += `<div class="card">`;
            txt += `    <div class="card-body">`;
            txt += `        <div class="row">`;
            txt += `            <table class="table table-sm">`;
            txt += `                <thead>`;
            txt += `                    <tr>`;
            txt += `                        <th scope="col" width="5%" style="border-top: none !important;text-align:center"></th>`;
            txt += `                        <th scope="col" width="35%" style="border-top: none !important;">Kitap Adı</th>`;
            txt += `                        <th scope="col" width="35%" style="border-top: none !important;">Yazarı</th>`;
            txt += `                        <th scope="col" width="10%" style="border-top: none !important;">Dosya</th>`;
            txt += `                        <th class="text-end px-4" scope="col" width="15%" style="border-top: none !important;">İşlem</th>`;
            txt += `                     </tr>`;
            txt += `                </thead>`;
            $.each(data, function (index, value) {
                txt += `            <tbody>`;
                if (value.aktifMi == 0)
                    txt += `            <tr class="inactive">`;
                else
                    txt += `            <tr>`;
                txt += `                    <td scope="row" class="text-center">${index + 1} </td>`;
                txt += `                    <td>${value.kitapAdi}</td>`;
                txt += `                    <td>${value.yazarAdi}</td>`;
                txt += `                    <td><a href="../${value.dosya}" target="_blank"><i class="far fa-file-pdf fa-2x"></i></a></td>`;
                txt += `                    <td class="text-end">`;
                txt += `                        <button class="btn btn-one btn-sm bg-info btnUpdate" data-id="${value.id}" title="Güncelle" data-bs-toggle="modal" data-bs-target="#addUpdateModal"><i class="fas fa-pen"></i></button>`;
                txt += `                        <button class="btn btn-one btn-sm bg-danger btnDelete" data-id="${value.id}" title="Sil"><i class="fas fa-trash"></i></button>`;
                txt += `                    </td>`;
                txt += `                </tr>`;
                txt += `            </tbody>`;
                txt += `        </div>`;
                txt += `    </div>`;
                txt += `</div>`;
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

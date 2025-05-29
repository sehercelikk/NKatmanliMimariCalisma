$(document).ready(function () {
    GetYazarlar('.data-area');
})

$(document).on('click', '#btnGetHidden', function () {
    GetYazarlar('.data-area');
})


$(document).on('click', '.btnUpdate', async function () {
    $('#addUpdateModalLabel').html("Yazar Güncelle");
    var id = $(this).data("id");
    var result = await GetFunction(`YazarGuncelle/${id}`);
    $('#yazarId').val(id);
    $('#YazarAdi').val(result.yazarAdi);
    $('.status').show();
    if (result.aktifMi == 1)
        $('#AktifMi').prop('checked', true);
    else
        $('#AktifMi').prop('checked', false);
})

$(document).on('change', ".aktifMi", function () {
    var durum = 0;
    if ($(this).is(':checked'))
        durum = 1;
    var formData = new FormData();
    formData.append("id", $(this).data("id"));
    formData.append("AktifMi", durum);
    AjaxAddUpdate("YazarDurumGuncelle", formData, '#btnGetHidden', '.btn-close');
})


$(document).on('click', '.btnAddModal', function () {
    $('#addUpdateModalLabel').html("Yazar Ekle");
    $('.status').hide();
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
    if ($('#yazarId').val() == "" || $('#yazarId').val() == null)
        baslik = "Yazar Ekleme İşlemi";
    else
        baslik = "Yazar Güncelleme İşlemi";
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
            formData.append("Id", $('#yazarId').val());
            formData.append("YazarAdi", $('#YazarAdi').val());
            formData.append("AktifMi", $('#AktifMi').is(':checked'));
            if (!$('#yazarId').val())
                AjaxAddUpdate("YazarEkle", formData, '#btnGetHidden', '.btn-close');
            else
                AjaxAddUpdate("YazarGuncelle", formData, '#btnGetHidden', '.btn-close');
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
            AjaxDelete("YazarSil", formData, '#btnGetHidden', '');
        }
    });
})
function GetYazarlar(element) {
    $.ajax({
        type: 'GET',
        url: "GetYazarlar",
        success: function (data) {
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
            txt += `                            <th scope="col" width="40%" style="border-top: none !important;">AdSoyad</th>`;
            txt += `                            <th class="text-end px-4" scope="col" width="40%"  style="border-top:none !important;">İşlem</th>`;
            txt += `                            <th scope="col" width="15%" style="border-top: none !important;"></th>`;
            txt += `                         </tr>`;
            txt += `                     </thead>`;
            $.each(data, function (index, value) {
                txt += `                <tbody>`;
                txt += `                <tr>`;
                txt += `                        <td scope="row" class="text-center">${index + 1} </td>`;
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


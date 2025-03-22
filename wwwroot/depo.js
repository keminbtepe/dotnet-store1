$(document).ready(function () {

    //Sayfa hazır olduğunda çalışacak kodlar buraya yazılır.

    ListeSayfasiniGetir(); // Sayfa yüklendiğinde çalışacak olan fonksiyon 
});



$(document).on("click", ".DeleteDepoBtn", function () {

    // Sil Butonuna tıklayınca çalışacak kod

    var butonunIdsi = $(this).data('kursatid');
      
    // "Depo/Delete" ye DELETE atar ama butonun id si ile
    $.ajax({
        url: '/Depo/Delete',
        type: 'DELETE',
        data: { id: butonunIdsi }, // Sil olduğu için Sile basılan butonun id si gidecek
        success: function (response) {

            ListeSayfasiniGetir();
        },
        error: function (error) {

            alert('Kayıt silinirken hata');
        }
    });

});

$(document).on("click", ".EditDepoBtn", function () {

    // Düzenle Butonuna tıklayınca çalışacak kod

    var butonunIdsi = $(this).data('kursatid');
     

    // "Depo/Form" a POST atar ama butonun id si ile
    $.ajax({
        url: '/Depo/Form',
        type: 'POST',
        data: { id: butonunIdsi }, // Düzenle olduğu için düzenleye basılan buyonun id si gidecek
        success: function (FormHtml) {

            // Mesaj kutusu açar
            bootbox.dialog({
                title: "Depo Formu",
                message: FormHtml // POST dan dönen sonucu kutunun içine koy
            });

        }
    });

});

$(document).on("click", "#AddDepoBtn", function () {

    // Ekle butonuna tıklayınca çalışan kod


    // "Depo/Form" a POST atar 
    $.ajax({
        url: '/Depo/Form',
        type: 'POST',
        data: { id: 0 },
        success: function (FormHtml) {
             
           // Mesaj kutusu açar
            bootbox.dialog({
                title: "Depo Formu",
                message: FormHtml // POST dan dönen sonucu kutunun içine koy
            });

        }
    });
     
});


$(document).on("submit", "#DepoForm", function (e) {
    e.preventDefault(); // Varsayılan form submit işlemini durdur

    // Form submit olurken çalışacak kod

    var form = $(this);
    var formData = form.serialize();
     
    $.ajax({
        url: '/Depo/Save',
        type: 'POST',
        data: formData,
        success: function (response) {

            bootbox.hideAll();
            ListeSayfasiniGetir();

        },
        error: function (error) {

            bootbox.hideAll();
            ListeSayfasiniGetir();
            alert("form kaydedilirken hata");
        }
    });
   
});


$(document).on("click", "#ListeYenileBtn", function () { 

    ListeSayfasiniGetir();
});

function ListeSayfasiniGetir() {

    $('#ListePartialViewiGelenDiv').html(' <span> Yükleniyor.... </span>');

    $.ajax({
        url: '/Depo/List',
        type: 'POST',
        success: function (ListHtml) {


            $('#ListePartialViewiGelenDiv').html(ListHtml);

        }
    });
}























//$.ajax({
//    url: '/Kategori/GetKategori',
//    type: 'GET',
//    success: function (response) {
//        console.log(response);
//    }
//});
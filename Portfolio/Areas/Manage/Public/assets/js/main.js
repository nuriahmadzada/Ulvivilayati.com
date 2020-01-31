$(document).ready(function(){

    $("#room .room-list .edit").click(function(){
        $("#room .room-list").hide();
        $("#room .editing").show();
        $("#room .header h1").text("Redaktə Et");
        $("#room .header .btn-primary").show();
    });

    $("#room .room-list .etrafli").click(function(){
        $("#room .room-list").hide();
        $("#room .details").show();
        $("#room .header h1").text("Vakansiya Ətraflı Baxış");
        $("#room .header .btn-primary").show();
    });

    $("#room .header .yarat").click(function(){
        $("#room .room-list").hide();
        $("#room .editing").hide();
        $("#room .create").show();
        $("#room .header h1").text("Yenisini Yarat");
        $("#room .header .btn-primary").show();
    });

    // $("#room .header .btn-primary").click(function(){
    //     $("#room .room-list").show();
    //     $("#room .create").hide();
    // });

    $("#customer .header yarat").click(function(){
        $("#customer .customer-list").hide();
        $("#customer .create").show();
        $("#customer .header h1").text("Create");
        $("#customer .header .btn-primary").show();
    });

});
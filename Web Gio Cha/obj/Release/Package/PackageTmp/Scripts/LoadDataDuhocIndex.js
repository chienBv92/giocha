var homeController = {
    init: function () {
        homeController.loadData();
        $('#loadingDataDuhoc').show();
    },
    loadData: function () {
        var data = WebDuhoc.utility.getDataByAjax('/ManageNews/LoadDataDuhoc');
        var html = "";
        $('.divDuhoc #loadingDataDuhoc').hide();
        if (data != null && data != undefined && data.length > 0) {
           
            for (var i = 0; i < data.length; i++) {

                html += ' <div class="formDuhoc">'
                    + buildActionLinkDuhoc(data[i].NEWS_ID, data[i].META_TITLE, data[i].TITLE, data[i].CONTENT, data[i].PICTURE, data[i].UPD_DATE, 'special-wrap')
                    + ' </div>';
            }
        }

        $('.divDuhoc .infoDuhoc').html(html);

        var dataNew = WebDuhoc.utility.getDataByAjax('/ManageNews/LoadDataNew');
        var html = "";

        if (dataNew != null && dataNew != undefined && dataNew.length > 0) {
            for (var i = 0; i < dataNew.length; i++) {

                html += ' <div class="formDuhoc">'
                    + buildActionLink(dataNew[i].NEWS_ID, data[i].META_TITLE, dataNew[i].TITLE, 'special-wrap')
                    + ' </div>';
            }
        }

        $('.divDuhoc .infoTinTuc').append(html);
    }
}

function buildActionLink(id, metaTitle, title) {
    var html = '';
    var link = "/tin-tuc/" + metaTitle + "/" + id;
    {
        html += '<a href="' + link + '" class="NewsView link" data-newsid="' + id + '">' + title + '</a>';
    }
    return html;
}

function buildActionLinkDuhoc(id, metaTitle, title, content, image, dateUp) {
    var html = '';
    var link = "/tin-tuc/" + metaTitle + "/" + id;
    var imglink = image != null ? image : "/UploadImage/images/logo.png";
    {
        html += '<a href="' + link + '" class="NewsView link" data-newsid="' + id + '" title="' + title + '">'
        html += '<div class="inner-img" style="overflow: hidden; position: relative;">'
        html += '<img class="img-dsp" src="' + imglink + '" title="' + title + '">'
        html += '</div>'
        html += '<label>' + dateUp + '</label> ' + title + '</a>'
    }
    return html;
}
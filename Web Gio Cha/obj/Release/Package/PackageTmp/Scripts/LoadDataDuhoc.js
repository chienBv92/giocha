var homeconfig = {
    pageSize: 5,
    pageIndex: 1,
}

var duhocController = {
    init: function () {
        duhocController.loadData();
        $('#loadingPopup').show();
    },
    loadData: function (changePageSize) {
        var name = $('#txtNameS').val();
        var categoryId = $('#CATEGORY_ID').val();
        $.ajax({
            url: '/ManageNews/GetNewsList',
            type: 'POST',
            data: {
                name: name,
                page: homeconfig.pageIndex,
                pageSize: homeconfig.pageSize,
                categoryId: categoryId
            },
            dataType: 'json',
            success: function (response) {
                if (response.status) {
                    $('#loadingPopup').hide();
                    var data = response.data;
                    var html = '';
                    var template = $('#data-template').html();
                    $.each(data, function (i, item) {
                        html += ' <div class="formDuhoc">'
                               + buildActionLinkDuhoc(item.NEWS_ID, item.META_TITLE, item.TITLE, item.CONTENT, item.DESCRIPTION, item.PICTURE, item.UPD_DATE, 'special-wrap')
                               + ' </div>';
                    });
                    $('#Listduhoc .infoDuhoc').html(html);
                    duhocController.paging(response.totalRow, function () {
                        duhocController.loadData();
                    }, changePageSize);
                }
            }
        })
    },
    paging: function (totalRow, callback, changePageSize) {
        var totalPage = Math.ceil(totalRow / homeconfig.pageSize);

        //Unbind pagination if it existed or click change pagesize
        if ($('#pagination a').length === 0 || changePageSize === true) {
            $('#pagination').empty();
            $('#pagination').removeData("twbs-pagination");
            $('#pagination').unbind("page");
        }

        $('#pagination').twbsPagination({
            totalPages: totalPage,
            first: "Đầu",
            next: "Tiếp",
            last: "Cuối",
            prev: "Trước",
            visiblePages: 10,
            onPageClick: function (event, page) {
                homeconfig.pageIndex = page;
                setTimeout(callback, 200);
            }
        });
    }
}

function buildActionLinkDuhoc(id, metaTitle, title, content, description, image, dateUp) {
    var html = '';
    var link = "/tin-tuc/" + metaTitle + "/" + id;
    var imglink = image != null ? image : "/UploadImage/images/logo.png";
    {
        html += '<a href="' + link + '" class="NewsView link" data-newsid="' + id + '" title="' + title + '">'
        html += '<div class="inner-img" style="overflow: hidden; position: relative;">'
        html += '<img class="img-dsp" src="' + imglink + '" title="' + title + '">'
        html += '</div>'
        html += '<div style="overflow: hidden; position: relative;">'
        html += '<h3 class="titleText">' + title + '</h3>'
        html += '<p class="descript-dsp">' + description + '</p>'
        html += '<label class="date-dsp">' + dateUp + '</label> '
        html += '</div>'

        html += '</a>'
    }
    return html;
}
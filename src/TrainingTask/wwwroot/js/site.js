(function () {
    ('#Submit').on('click', function (evt) {
        evt.preventDefault();
        $.ajax({
            url: '',
            data: new FormData(document.forms[0]),
            contentType: false,
            processData: false,
            type: 'post',
            success: function () {
                alert('Successfully uploaded');
            }
        });
    });
});

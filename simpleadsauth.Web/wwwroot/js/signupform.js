$(() => {


    $("input").on('input', function () {
        ensureFormIsValid();

    });

   

   
    function ensureFormIsValid() {
        const name = $("#name").val().trim();
        const number = $("#number").val().trim();
        const email = $("#email").val().trim();
        const password = $("#password").val().trim();
        

        let isValid = name && number && email && password;
        $("#sign-up").prop('disabled', !isValid);

    }

});
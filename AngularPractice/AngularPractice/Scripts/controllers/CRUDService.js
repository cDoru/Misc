app.factory('crudService', function ($http) {
    var service =
    {
        post: function (data) {
            $http({
                method: 'post',
                url: '/Home/Post',
                data: data,
                headers: { 'Content-Type': 'application/json' },
                });

        }
    };
    return service;
});
app.factory('sortService', function () {
    return {
        setSort: function (headers, data) {
            var sortProperty;
            var sortDirection;
            var sortIndex;

            var sorter = function () {
                return function (x1, x2) {
                    var result = (x1[sortIndex] < x2[sortIndex]) ? -1 : (x1[sortIndex] > x2[sortIndex]) ? 1 : 0;
                    return result * sortDirection;
                };
            };

            return function(property) {
                if (property === sortProperty) {
                    sortDirection *= -1;
                } else {
                    sortDirection = 1;
                }

                sortProperty = property;

                var findIndex = function () {
                    var returnIndex = -1;
                    for (i in headers) {
                        if (headers[i] === sortProperty) {
                            headers[i].sortDirection = sortDirection;
                            returnIndex = i;
                        } else {
                            headers[i].sortDirection = 0;
                        }
                    }
                    return returnIndex;
                };

                sortIndex = findIndex();

                data = data.sort(sorter());
            };
        }
    };
});


app.controller('mainController', ['$scope', 'myService', 'crudService', function ($scope, myService, crudService) {
    $scope.title = myService.title;

    $scope.addSomething = function() {
        myService.addAsterix($scope);
        $scope.$apply();
    };

    $scope.testPost = function () {
        crudService.post({ Text: 'Hello' });
    };

}]);

app.controller('tableController', ['$scope', 'sortService', function ($scope, sortService) {
    $scope.headers = [
        { title: "Title", sortDirection: 0 },
        { title: "Value", sortDirection: 0 },
        { title: "Description", sortDirection: 0 }
    ];

    $scope.data =
    [
        ["One", 1, "Text"],
        ["Two", 2, "Some Other Text"],
        ["Three", 3, "Text"],
        ["Four", 4, "Text"],
        ["Five", 5, "Text"],
        ["Six", 6, "This is a note"],
        ["Seven", 7, "Text"],
        ["Eight", 8, "Text"]
    ];

    $scope.sort = sortService.setSort($scope.headers, $scope.data);
}]);

app.factory('myService', function() {
    var service =
    {
        title: "Title",
        addAsterix: function(scope) {
            scope.title = scope.title + "*";
        }
    };
    return service;
});





app.directive("coolcontrol", function() {
    return {
        restrict: 'AE',
        //replace: true,
        scope: { title: "=" },
        link: function(scope, element) {
            element.bind("mouseenter", function() {
                scope.title = scope.title + "*";
                scope.$apply();
            });
        },
        template: '<h2>Wow, {{title}}</h2>'
    };
});


app.directive("myButton", function() {
    return{
        restrict: "E",
        scope: {
            title: "@",
            clicker: '&'
        },
        link: function (scope, element, attrs) {
            element.on('mouseenter', function() {

                $(element).animate({
                    transform: 'scale(1.2,1.2)'
                });
            });
            element.on('mouseleave', function() {
                //$(element).animate({
                //    transform: 'scale(1.0,1.0)'
                //});
            });

            element.on('click', function () {
                scope.clicker()();
            });
        },
        template: "<div class='btn btn-info'>{{title}}</div>"
    }
});

app.directive("myDiv", function() {
    return {
        restrict: "E",
        transclude: true,
        controller: function($scope) {
          $scope.getColor = function() {
              return "blue";
          }  
        },
        link: function(scope, element, attrs) {
            element.bind('mouseenter', function() {
                element.css('color', scope.getColor());
            });
            element.bind('mouseleave', function () {
                element.css('color', 'rgb(194, 47, 47)');
            });
        },
        template: "<div class='well bg-well'><div ng-transclude></div></div>"
    };
});




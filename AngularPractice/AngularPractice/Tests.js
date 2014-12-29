
/// <reference path="Scripts/jasmine.js" />
/// <reference path="Scripts/angular.js" />
/// <reference path="Scripts/angular-mocks.js" />
///<reference path="Scripts/controllers/app.js" />
///<reference path="Scripts/controllers/mainController.js" />

describe("app", function () {
    var scope, ctrl;

    beforeEach(function() {
        module('app');
    });

    beforeEach(inject(function ($rootScope, $controller) {
        $rootScope = $rootScope;
        scope = $rootScope.$new();
        var mockMyService = { title: "mock" }; 

        ctrl = $controller('mainController', { '$scope': scope, "myService": mockMyService });
    }));

    it('scope title should be set', function () {
        expect(scope.title).toBe("mock");
    });
});



    
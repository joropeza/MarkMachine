

angular.module('app', ['ui.router', 'markets', 'ngMockE2E'])
.run(function ($rootScope, $state, $location, $stateParams, $httpBackend) {
    $rootScope.$on("$stateChangeStart", function (event, toState, toParams, fromState, fromParams) {

        
    });

    var JSON = {};

    	

    //console.log(JSON);


    $httpBackend.whenGET('/api/Markets/TopTen').respond(JSON);

    $httpBackend.whenGET(/app/).passThrough();
    $httpBackend.whenGET(/js/).passThrough();
    $httpBackend.whenGET(/api/).passThrough();
    $httpBackend.whenPOST(/api/).passThrough();
});






  // .config(['$routeProvider', '$locationProvider', function ($routeProvider, $locationProvider) {
  // }])

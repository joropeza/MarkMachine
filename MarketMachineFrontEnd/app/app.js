


angular.module('app', ['markets', 'ngMockE2E', 'ngRoute'])
.run(function ($rootScope, $location, $httpBackend) {
    

    var JSON = [{ "$id": "1", "symbol": "SPY", "name": "Spyder S&P" }];

    	

    //console.log(JSON);


    $httpBackend.whenGET('/api/Markets/TopTen').respond(JSON);

    $httpBackend.whenGET(/app/).passThrough();
    $httpBackend.whenGET(/js/).passThrough();
    $httpBackend.whenGET(/api/).passThrough();
    $httpBackend.whenPOST(/api/).passThrough();
});






  // .config(['$routeProvider', '$locationProvider', function ($routeProvider, $locationProvider) {
  // }])

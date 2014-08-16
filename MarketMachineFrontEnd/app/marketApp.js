
'use strict'

angular.module('markets', ['ngCookies'])

.config(['$stateProvider', '$urlRouterProvider', function ($stateProvider, $urlRouterProvider) {
	var base_url = "/VEFrontEnd/";
	$stateProvider
	.state('home', {
		url: "/",
		controller: 'HomeController',
		authenticate: false
	})
	$urlRouterProvider.otherwise("/");
}])

.controller('HomeController', ['$scope', '$stateParams', '$state', , function ($scope, $stateParams, $state) {

	$scope.message = "hello jonny";
	
}])

.service('marketService', ['$http', '$location', function ($http, $location) {

	var myService = {
		topTen: function () {
            // $http returns a promise, which has a then function, which also returns a promise


            //var promise = $http.get('/Api/Videos/Enhancements?GUID=c0890c91-8e0d-45ec-9e92-8648761b1238.mp4').then(function (response) {
            	var promise = $http.get('/api/Videos/' + videoGUID + '/Watch').then(function (response) {

                // The then function here is an opportunity to modify the response
                console.log(response);
                // The return value gets picked up by the then in the controller.
                return response.data;
              });
            // Return the promise to the controller
            return promise;
          }
        };

        return myService;
 }]);






var app = angular.module('techtutor', ['ngRoute', 'ngAnimate', 'LocalStorageModule', 'cgBusy', 'ui.bootstrap', 'angular-loading-bar', 'ngMessages']);

app.config(function ($routeProvider) {
  

    $routeProvider.when("/movies", {
        controller: "movieController",
        templateUrl: "app/views/movie.html"
    });

    $routeProvider.when("/movies/:movid", {
        controller: "moviecreateController",
        templateUrl: "app/views/moviecreate.html"
    });

    $routeProvider.otherwise({ redirectTo: "/movies" });
});
app.run(['$rootScope', '$location', 'localStorageService', function ($rootScope, $location, localStorageService) {
}]);
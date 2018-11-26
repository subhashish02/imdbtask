'use strict';
app.controller('movieController', ['$scope', '$rootScope', '$filter', 'localStorageService', '$location', '$uibModal', '$routeParams', 'genericService', function ($scope, $rootScope, $filter, localStorageService, $location, $uibModal, $routeParams, genericService) {
    debugger;
    $scope.reverseSort = false;
    $scope.filterData = "";
    $scope.movieList = [];
    $scope.tableData = [];
    try {
        $scope.Getmovies = function () {
            $scope.busymsg = "Getting movie Please Wait.."
            $scope.saveBusy = true;
            $scope.myPromise = genericService.genericFunction('GET', 'Movie', {}, 0);
            $scope.myPromise.then(function (results) {
                debugger;
                $scope.movieList = results;
                $scope.saveBusy = false;
            });
        }

        $scope.deletemovie = function (movid) {
            $scope.busymsg = "deleting session Please Wait.."
            $scope.saveBusy = true;
            $scope.myPromise = genericService.genericFunction('DELETE', 'movie', {}, movid);
            $scope.myPromise.then(function (result) {

                if (result.bstatus == true) {
                    $scope.msuccess = true;
                    $scope.isOpen = false;
                    $scope.msg = "movie deleted successfully.";
                    $scope.getSessions();
                }
                else {
                    $scope.msuccess = false;
                    $scope.msg = result.bmsg;
                }

                $scope.saveBusy = false;
            });
        }
        $scope.createClick = function (mode, movie) {
            
            if (mode === "NEW") {
                $location.url("/movies/" + -1);
            } else {
                $rootScope.movie = movie;
                $location.url("/movies/" + movie.movid);
            }
        };
        $scope.Getmovies();
    }
    catch (e) {
    }



}]);
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

        $scope.deleteClick = function (movid) {
            $scope.busymsg = "deleting movie Please Wait.."
            $scope.saveBusy = true;
            $scope.myPromise = genericService.genericFunction('DELETE', 'Movie', {}, movid);
            $scope.myPromise.then(function (result) {

                if (result.status != "error") {
                    $scope.msuccess = true;
                    $scope.msg = "movie deleted successfully.";
                    var findex = _.findIndex($scope.movieList, function (obj) { return obj.movid === movid; });
                    if (findex > -1) {
                        $scope.movieList.splice(findex, 1);
                    }
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
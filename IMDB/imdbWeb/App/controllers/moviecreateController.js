'use strict';
app.controller('moviecreateController', ['$scope', '$rootScope', '$filter', 'localStorageService', '$location', '$uibModal', '$routeParams','genericService', function ($scope, $rootScope, $filter, localStorageService, $location, $uibModal, $routeParams, genericService) {
    debugger;
    try {
        $scope.message = "";
        $scope.movie = {};
        $scope.actors = [];
        $scope.producers = [];
        $scope.selactor = {};
        $scope.movid = +$routeParams.movid;
        $scope.msuccess = false;
        $scope.msg = "";
        $scope.cancel = function () {

        }

        $scope.addMovieActor = function () {
            var findex = _.findIndex($scope.movie.actors, function (obj) { return obj.actid == $scope.selactor.actid; });
            if (findex === -1) {
                $scope.movie.actors.push($scope.selactor);
            }        
        }
        $scope.addNewActor = function () {
            var modalInstance = $uibModal.open({
                templateUrl: 'app/views/actorcreate.html',
                controller: 'actorcreateController',
                size: 'md',
                resolve: {
                }
            });
            
            modalInstance.result.then(function (obj) {
                debugger;
                if (obj.actid) {
                    $scope.actors.push(obj);
                }
            });
        }

        $scope.addNewProducer = function () {
            var modalInstance = $uibModal.open({
                templateUrl: 'app/views/producercreate.html',
                controller: 'producercreateController',
                size: 'md',
                resolve: {
                }
            });

            modalInstance.result.then( function (obj) {
                if (obj.proid) {
                    $scope.producers.push(obj);
                }
            });
        }

        $scope.removeMovieActor = function (actid) {
            var findex = _.findIndex($scope.movie.actors, function (obj) { return obj.actid === actid; });
            if (findex != -1) {
                $scope.movie.actors.splice(findex,1);
            }
        }

        $scope.getMovie = function () {
            debugger;
            $scope.busymsg = "Getting movie Please Wait.."
            $scope.saveBusy = true;
            $scope.myPromise = genericService.genericFunction('GET', 'Movie', {}, $scope.movid);
            $scope.myPromise.then(function (result) {
                debugger;
                $scope.movie = result;
                $scope.saveBusy = false;
                $scope.GetActors();
            });
        }

        $scope.GetActors = function () {
            $scope.busymsg = "Getting movie Please Wait.."
            $scope.saveBusy = true;
            $scope.myPromise = genericService.genericFunction('GET', 'Actor', {}, 0);
            $scope.myPromise.then(function (results) {
                debugger;
                $scope.actors = results;
                $scope.actors.splice(0, { actid: -1, actname:'Select' });
                $scope.selactor = $scope.actors[0]; 
                $scope.saveBusy = false;
                $scope.GetProduces();
            });
        }

        $scope.GetProduces = function () {
            $scope.busymsg = "Getting movie Please Wait.."
            $scope.saveBusy = true;
            $scope.myPromise = genericService.genericFunction('GET', 'Producer', {}, 0);
            $scope.myPromise.then(function (results) {
                $scope.producers = results;
                $scope.producers.splice(0, { proid: -1, proname: 'Select' });
                $scope.movie.producer = $scope.producers[0]; 
                $scope.saveBusy = false;
            });
        }

        $scope.savemovie = function () {
            debugger;
            $scope.message = "";
            if ($scope.movid === -1) {
                $scope.busymsg = "Creating New movie. Please Wait..."
                $scope.saveBusy = true;
                $scope.myPromise = genericService.genericFunction('POST', 'Movie', $scope.movie, 0);
                $scope.myPromise.then(function (results) {

                    debugger;
                    $scope.message = results.message;
                    $scope.saveBusy = false;
                    $location.url("/movies/" + results.lid);
                    if (results.status != 'error') {
                        $scope.msuccess = false;
                        $scope.msg = results.message;
                    }
                });
            }
            else {
                $scope.busymsg = "Updating movie Details. Please Wait..."
                $scope.saveBusy = true;
                $scope.myPromise = genericService.genericFunction('PUT', 'Movie', $scope.movie, $scope.movid);
                $scope.myPromise.then(function (results) {
                    $scope.message = results.message;
                    $scope.saveBusy = false;
                    $scope.msuccess = true;
                    $scope.msg = "movie saved successfully.";
                    if (results.status != 'error') {
                        $scope.msuccess = false;
                        $scope.msg = results.message;
                    }
                });
            }
        }

        $scope.onFileChange = function (fileInput) {
            $scope.message = "";
            $scope.msuccess = false;
            if (fileInput.files[0]) {
                var file = fileInput.files[0];
                if (file) {
                    var reader = new FileReader();
                    reader.onload = (event) => {
                        debugger;
                        var b64 = event.target.result;
                        $scope.movie.posterImg = b64;
                    }
                    reader.readAsDataURL(file);
                }
            }
        }


        if (!$scope.movid || $scope.movid ===-1) {
            $scope.modaltitle = "Create New movie";
            $scope.movie = {
                actors: [], producers: {} };
            $scope.GetActors();
        }
        else {
            $scope.movie = angular.copy($rootScope.movie);
            if (!$scope.movie) {
                $scope.getMovie();
            } else {
                $scope.GetActors();
            }
            $scope.modaltitle = "Edit movie Details";
        }
        
    }
    catch (e) {
        debugger;
    }
}]);
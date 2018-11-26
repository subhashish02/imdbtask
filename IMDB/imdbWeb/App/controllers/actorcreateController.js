'use strict';
app.controller('actorcreateController', ['$scope', '$rootScope', '$filter', 'localStorageService', '$location', '$modalInstance', 'genericService', function ($scope, $rootScope, $filter, localStorageService, $location, $modalInstance, genericService) {
    debugger;
    try {
        $scope.message = "";
        $scope.actor = {};


        $scope.cancel = function () {
            $modalInstance.dismiss('cancel');
        };
        $scope.cancel();

        $scope.createactor = function () {
            debugger;
            $scope.message = "";
            $scope.busymsg = "Creating New actor. Please Wait..."
            $scope.saveBusy = true;
            $scope.myPromise = genericService.genericFunction('POST', 'Actor', $scope.actor, 0);
            $scope.myPromise.then(function (results) {

                debugger;
                $scope.message = results.message;
                $scope.saveBusy = false;
                if (results.status != 'error') {
                    $scope.actor.actid = results.lid;
                    $modalInstance.close($scope.actor);
                }
                  
            });
        }


        $scope.deleteactor = function (actid) {
            $scope.busymsg = "deleting session Please Wait.."
            $scope.saveBusy = true;
            $scope.myPromise = genericService.genericFunction('DELETE', 'Actor', {}, scope.actor.actid);
            $scope.myPromise.then(function (result) {

                if (result.bstatus == true) {
                    $scope.msuccess = true;
                    $scope.isOpen = false;
                    $scope.msg = "actor deleted successfully.";
                    
                }
                else {
                    $scope.msuccess = false;
                    $scope.msg = result.bmsg;
                }

                $scope.saveBusy = false;
            });
        }
    }
    catch (e) { }
}]);
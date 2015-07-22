app.controller('functionalController', ['$scope', 'dataService', function ($scope, db) {

    function refreshRecord(page) {
        return db.system.getFunctionals(page).success(function (result) {
            $scope.industries = result;
        });
    }

    $scope.save = function (record) {
        db.system.addFunctional(record).success(refreshRecord);
    }

    $scope.update = function (record) {
        db.system.updateFunctional(record).success(refreshRecord);
    }

    $scope.delete = function (record) {
        db.system.deleteFunctional(record).success(refreshRecord);
    }

    $scope.toggle = function (i) {
        i.editMode = !i.editMode;
    };

    refreshRecord();

}]);
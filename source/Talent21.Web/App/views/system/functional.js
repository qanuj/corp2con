app.controller('functionalController', ['$scope', 'dataService', function ($scope, db) {

    $scope.save = function (record) {
        $('input[type=text]').each(function () {
            $(this).val('');
        });
        db.system.addFunctional(record).success(refreshRecord);
    }

    $scope.update = function (record) {
        db.system.updateFunctional(record).success(refreshRecord);
    };

    $scope.delete = function (record) {
        db.system.deleteFunctional(record).success(refreshRecord);
    };

    $scope.toggle = function (record) {
        record.editMode = !record.editMode;
    };

    refreshRecord();

    function refreshRecord() {
        return db.system.getFunctionals().success(function (result) {
            $scope.records = result;
        });
    }

}]);


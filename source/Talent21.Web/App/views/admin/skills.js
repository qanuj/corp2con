app.controller('jobskillsController', ['$scope', 'dataService', function ($scope, db) {

    $scope.save = function (record) {
        $('input[type=text]').each(function () {
            $(this).val('');
        });
        db.system.addSkill(record).success(refreshRecord);
    }

    $scope.update = function (record) {
        db.system.editSkill(record).success(refreshRecord);
    };

    $scope.delete = function (record) {
        db.system.deleteSkill(record).success(refreshRecord);
    };

    $scope.toggle = function (record) {
        record.editMode = !record.editMode;
    };

    refreshRecord();

    function refreshRecord() {
        return db.system.getSkills().success(function (result) {
            $scope.records = result;
        });
    }

}]);


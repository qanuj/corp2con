app.controller('adminSkillsController', ['$scope', 'dataService', '$routeParams','$rootScope', function ($scope, db, params,$rootScope) {
        $scope.$on('$viewContentLoaded', function () {
        // initialize core components
        Metronic.initAjax();
    });

    // set sidebar closed and body solid layout mode
    $rootScope.settings.layout.pageBodySolid = false;
    $rootScope.settings.layout.pageSidebarClosed = false;

    $scope.title = "Skills";

    $scope.navigate = function (page) {

        db.system.pagedSkill(page).success(function (result) {
            $scope.currentPage = page || 1;
            $scope.pages = Math.ceil(result.count / db.pageSize);
            $scope.records = result.items;
        });


        $scope.save = function (record) {
            $('input[type=text]').each(function () {
                $(this).val('');
            });
            db.system.addSkill(record).success($scope.navigate());
        }

        $scope.update = function (record) {
            db.system.editSkill(record).success($scope.navigate(params.page));
        };

        $scope.delete = function (record) {
            db.system.deleteSkill(record).success($scope.navigate(params.page));
        };

        $scope.toggle = function (record) {
            record.editMode = !record.editMode;
        };

        function refreshRecord() {
            return db.system.getSkills().success(function (result) {
                $scope.records = result;
            });
        }
    }
    $scope.navigate(params.page);
}]);


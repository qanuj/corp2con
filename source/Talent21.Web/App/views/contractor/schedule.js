app.controller('contractorScheduleController', ['$scope', 'dataService', '$stateParams', function ($scope, db, params) {
    $scope.title = "Manage Schedule";
    $scope.today = new Date();

    $scope.navigate = function (page) {
        db.contractor.pagedSchedule(page).success(function (result) {
            $scope.currentPage = page || 1;
            $scope.pages = Math.ceil(result.count / db.pageSize);
            $scope.records = result.items;
        });
    }

    $scope.save = function (record) {
        console.log(record);
        db.contractor.createSchedule({
            start: record.date.startDate.format(),
            end: record.date.endDate.format(),
            company: record.company,
            isAvailable: record.isAvailable
        }).success($scope.navigate).finally(function () {
            $scope.start = '';
            $scope.end = '';
        });
    }

    $scope.update = function (s) {
        db.contractor.editSchedule(s).success($scope.navigate(params.page));
    };

    $scope.delete = function (s) {
        db.contractor.deleteSchedule(s).success($scope.navigate(params.page));
    };

    $scope.toggle = function (s) {
        s.editMode = !s.editMode;
    };

    $scope.navigate(params.page);

    $('#defaultrange').daterangepicker({
        opens: (Metronic.isRTL() ? 'left' : 'right'),
        format: 'MM/DD/YYYY',
        separator: ' to ',
        startDate: moment().subtract('days', 29),
        endDate: moment(),
        minDate: '01/01/2012',
        maxDate: '12/31/2018',
    },
        function (start, end) {
            $('#defaultrange input').val(start.format('MMMM D, YYYY') + ' - ' + end.format('MMMM D, YYYY'));
        }
    );


}]);
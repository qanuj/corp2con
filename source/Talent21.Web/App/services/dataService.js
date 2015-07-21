app.factory('dataService', ['$http', '$q', function ($http, $q) {
    var v = 'api/v1/';
    var factory = {
        pageSize: 50,
        contractor: {},
        job: {},
        company: {},
        system: {}
    };

    function calculatePaging(page, pageSize) {
        pageSize = pageSize || factory.pageSize;
        var pg = "&$top=" + pageSize;
        if (page > 1) {
            pg += "&$skip=" + ((page - 1) * pageSize);
        }
        return pg;
    }

    factory.role = document.querySelector('html').dataset.role;

    //For Contractors

    factory.contractor.paged = function () {
        return $http.get(v + 'contractor/paged');
    }

    factory.contractor.all = function () {
        return $http.get(v + 'contractor/all');
    }

    factory.contractor.get = function (id) {
        return $http.get(v + 'contractor/profile' + (!!id ? '/' + id : ''));
    }

    factory.contractor.update = function (formData) {
        return $http.put(v + 'contractor/profile', formData);
    }

    factory.contractor.search = function (query) {
        return $http.post(v + 'job/search', query);
    }

    factory.contractor.jobById = function (id) {
        return $http.get(v + 'contractor/job/' + id);
    }

    factory.contractor.ApplyToJob = function (id) {
        return $http.post(v + 'contractor/job/' + id + '/apply');
    }

    factory.contractor.favorite = function (id) {
        return $http.put(v + 'candidate/job/application/' + id + '/favorite');
    }

    factory.contractor.createSchedule = function (formData) {
        return $http.post(v + 'contractor/schedule', formData);
    }

    factory.contractor.editSchedule = function (formData) {
        return $http.put(v + 'contractor/schedule', formData);
    }
    factory.contractor.deleteSchedule = function (formData) {
        return $http.delete(v + 'contractor/schedule', formData);
    }

    factory.contractor.getSchedule = function (page) {
        return $http.get(v + 'contractor/schedule/all?$orderby=Id desc' + calculatePaging(page));
    }


    //For companies

    factory.company.paged = function () {
        return $http.get(v + 'company/paged?$inlinecount=allpages');
    }

    factory.company.all = function () {
        return $http.get(v + 'company/all');
    }

    factory.company.get = function (id) {
        return $http.get(v + 'company/profile' + (!!id ? '/' + id : ''));
    }

    factory.company.searchContractor = function (query) {
        return $http.post(v + 'company/search', query);
    }

    factory.company.update = function (formData) {
        return $http.put(v + 'company/profile', formData);
    }

    factory.company.myJobs = function () {
        return $http.get(v + 'company/job/all');
    }

    factory.company.limitJobs = function () {
        return $http.get(v + 'company/job/paged');
    }

    factory.company.search = function (query) {
        return $http.post(v + 'company/search', query);
    }
    //For Jobs

    factory.job.get = function (id) {
        return $http.get(v + 'company/job/' + id);
    }

    factory.job.paged = function (page) {
        return $http.get(v + 'company/job/paged?$orderby=Id desc' + calculatePaging(page));
    }

    factory.job.create = function (formData) {
        return $http.post(v + 'company/job', formData);
    }

    factory.job.update = function (record) {
        return $http.put(v + 'company/job', record);
    }

    factory.job.publish = function (id) { return $http.put(v + 'company/job/publish', { id: id }); }
    factory.job.cancel = function (id) { return $http.put(v + 'company/job/cancel', { id: id }); }
    factory.job.delete = function (id) { return $http.delete(v + 'company/job/delete', { id: id }); }


    factory.system.searchLocations = function (address) {
        return $http.get('//maps.googleapis.com/maps/api/geocode/json', { params: { address: address, sensor: false } });
    }

    factory.system.getSkills = function (q) {
        var uri = v + 'system/skill/all';
        if (q) uri += '?$filter=startswith(Title,\'' + q + '\')';
        return $http.get(uri);
    }

    factory.system.getIndustries = function () {
        return $http.get(v + 'system/industry/all');
    }

    factory.system.addIndustry = function (record) {
        return $http.post(v + 'system/industry/create', record);
    }
    factory.system.updateIndustry = function (record) {
        return $http.put(v + 'system/industry/update', record);
    }
    factory.system.deleteIndustry = function (record) {
        return $http.delete(v + 'system/industry/delete', record);
    }
    factory.system.addSkill = function (formData) {
        return $http.post(v + 'system/skill/create', formData);
    }
    factory.system.editSkill = function (formData) {
        return $http.put(v + 'system/skill/update', formData);
    }
    factory.system.deleteSkill = function (formData) {
        return $http.delete(v + 'system/skill/delete', formData);
    }
    factory.system.editIndustry = function (formData) {
        return $http.put(v + 'system/industry/update', formData);
    }
    factory.system.getFunctionalArea = function (page) {
        return $http.get(v + 'system/functional/all?$orderby=Id desc' + calculatePaging(page));
    }
    factory.system.addFunctionalArea = function (formData) {
        return $http.post(v + 'system/functional/create', formData);
    }
    factory.system.editFunctionalArea = function (formData) {
        return $http.put(v + 'system/functional/update', formData);
    }
    factory.system.deleteFunctionalArea = function (formData) {
        return $http.delete(v + 'system/functional/delete', formData);
    }
    return factory;
    return factory;
}]);
﻿//----------------------
// <auto-generated>
//     Generated using the NSwag toolchain v5.4.6091.41181 (http://NSwag.org)
// </auto-generated>
//----------------------

export class PersonsClient {
    baseUrl: string = undefined; 
    beforeSend: any = undefined; 

    constructor(baseUrl?: string) {
        this.baseUrl = baseUrl !== undefined ? baseUrl : "http://localhost:13452"; 
    }

    getAll(): Promise<Person[]> {
        return new Promise<Person[]>((resolve, reject) => {
            this.getAllWithCallbacks((result) => resolve(result), (exception, reason) => reject(exception));
        });
    }
    
    private getAllWithCallbacks(onSuccess?: (result: Person[]) => void, onFail?: (exception: string, reason: string) => void) {
        var url = this.baseUrl + "/api/Persons"; 

        var content = "";

        jQuery.ajax({
            url: url,
            beforeSend: this.beforeSend,
            type: "get",
            data: content,
            dataType: "text",
            headers: {
                "Content-Type": "application/json; charset=UTF-8"
            }
        }).done((data, textStatus, xhr) => {
            this.processGetAllWithCallbacks(url, xhr, onSuccess, onFail);
        }).fail((xhr) => {
            this.processGetAllWithCallbacks(url, xhr, onSuccess, onFail);
        });
    }

    private processGetAllWithCallbacks(url: string, xhr: any, onSuccess?: any, onFail?: any) {
        try {
            var result = this.processGetAll(xhr);
            if (onSuccess !== undefined)
                onSuccess(result);
        } catch (e) {
            if (onFail !== undefined)
                onFail(e, "http_service_exception");
        }
    }

    private processGetAll(xhr: any) {
        var data = xhr.responseText; 
        var status = xhr.status.toString(); 

        if (status === "200") {
            var result200: Person[] = null; 
            if (data !== undefined && data !== null && data !== "") {
                var resultData200 = data === "" ? null : jQuery.parseJSON(data);
                if (resultData200 && resultData200.constructor === Array) {
                    result200 = [];
                    for (let item of resultData200)
                        result200.push(Person.fromJS(item));
                }
            }
            return result200;
        }
        else
        {
            throw new Error("error_no_callback_for_the_received_http_status");
        }
    }

    add(person: Person): Promise<void> {
        return new Promise<void>((resolve, reject) => {
            this.addWithCallbacks(person, (result) => resolve(result), (exception, reason) => reject(exception));
        });
    }
    
    private addWithCallbacks(person: Person, onSuccess?: (result: void) => void, onFail?: (exception: string, reason: string) => void) {
        var url = this.baseUrl + "/api/Persons"; 

        var content = JSON.stringify(person ? person.toJS() : null);

        jQuery.ajax({
            url: url,
            beforeSend: this.beforeSend,
            type: "post",
            data: content,
            dataType: "text",
            headers: {
                "Content-Type": "application/json; charset=UTF-8"
            }
        }).done((data, textStatus, xhr) => {
            this.processAddWithCallbacks(url, xhr, onSuccess, onFail);
        }).fail((xhr) => {
            this.processAddWithCallbacks(url, xhr, onSuccess, onFail);
        });
    }

    private processAddWithCallbacks(url: string, xhr: any, onSuccess?: any, onFail?: any) {
        try {
            var result = this.processAdd(xhr);
            if (onSuccess !== undefined)
                onSuccess(result);
        } catch (e) {
            if (onFail !== undefined)
                onFail(e, "http_service_exception");
        }
    }

    private processAdd(xhr: any) {
        var data = xhr.responseText; 
        var status = xhr.status.toString(); 

        if (status === "204") {
            var result204: any = undefined; 
            return result204;
        }
        else
        {
            throw new Error("error_no_callback_for_the_received_http_status");
        }
    }

    get(id: string): Promise<Person> {
        return new Promise<Person>((resolve, reject) => {
            this.getWithCallbacks(id, (result) => resolve(result), (exception, reason) => reject(exception));
        });
    }
    
    private getWithCallbacks(id: string, onSuccess?: (result: Person) => void, onFail?: (exception: string, reason: string) => void) {
        var url = this.baseUrl + "/api/Persons/{id}"; 

        if (id === undefined || id === null)
            throw new Error("The parameter 'id' must be defined.");
        url = url.replace("{id}", encodeURIComponent("" + id)); 

        var content = "";

        jQuery.ajax({
            url: url,
            beforeSend: this.beforeSend,
            type: "get",
            data: content,
            dataType: "text",
            headers: {
                "Content-Type": "application/json; charset=UTF-8"
            }
        }).done((data, textStatus, xhr) => {
            this.processGetWithCallbacks(url, xhr, onSuccess, onFail);
        }).fail((xhr) => {
            this.processGetWithCallbacks(url, xhr, onSuccess, onFail);
        });
    }

    private processGetWithCallbacks(url: string, xhr: any, onSuccess?: any, onFail?: any) {
        try {
            var result = this.processGet(xhr);
            if (onSuccess !== undefined)
                onSuccess(result);
        } catch (e) {
            if (onFail !== undefined)
                onFail(e, "http_service_exception");
        }
    }

    private processGet(xhr: any) {
        var data = xhr.responseText; 
        var status = xhr.status.toString(); 

        if (status === "200") {
            var result200: Person = null; 
            if (data !== undefined && data !== null && data !== "") {
                var resultData200 = data === "" ? null : jQuery.parseJSON(data);
                result200 = resultData200 ? Person.fromJS(resultData200) : new Person();
            }
            return result200;
        }
        else
        {
            throw new Error("error_no_callback_for_the_received_http_status");
        }
    }

    delete(id: string): Promise<void> {
        return new Promise<void>((resolve, reject) => {
            this.deleteWithCallbacks(id, (result) => resolve(result), (exception, reason) => reject(exception));
        });
    }
    
    private deleteWithCallbacks(id: string, onSuccess?: (result: void) => void, onFail?: (exception: string, reason: string) => void) {
        var url = this.baseUrl + "/api/Persons/{id}"; 

        if (id === undefined || id === null)
            throw new Error("The parameter 'id' must be defined.");
        url = url.replace("{id}", encodeURIComponent("" + id)); 

        var content = "";

        jQuery.ajax({
            url: url,
            beforeSend: this.beforeSend,
            type: "delete",
            data: content,
            dataType: "text",
            headers: {
                "Content-Type": "application/json; charset=UTF-8"
            }
        }).done((data, textStatus, xhr) => {
            this.processDeleteWithCallbacks(url, xhr, onSuccess, onFail);
        }).fail((xhr) => {
            this.processDeleteWithCallbacks(url, xhr, onSuccess, onFail);
        });
    }

    private processDeleteWithCallbacks(url: string, xhr: any, onSuccess?: any, onFail?: any) {
        try {
            var result = this.processDelete(xhr);
            if (onSuccess !== undefined)
                onSuccess(result);
        } catch (e) {
            if (onFail !== undefined)
                onFail(e, "http_service_exception");
        }
    }

    private processDelete(xhr: any) {
        var data = xhr.responseText; 
        var status = xhr.status.toString(); 

        if (status === "204") {
            var result204: any = undefined; 
            return result204;
        }
        else
        {
            throw new Error("error_no_callback_for_the_received_http_status");
        }
    }

    /**
     * Gets the name of a person.
     * @id The person ID.
     * @return The person's name.
     */
    getName(id: string): Promise<string> {
        return new Promise<string>((resolve, reject) => {
            this.getNameWithCallbacks(id, (result) => resolve(result), (exception, reason) => reject(exception));
        });
    }
    
    private getNameWithCallbacks(id: string, onSuccess?: (result: string) => void, onFail?: (exception: string, reason: string) => void) {
        var url = this.baseUrl + "/api/Persons/{id}/Name"; 

        if (id === undefined || id === null)
            throw new Error("The parameter 'id' must be defined.");
        url = url.replace("{id}", encodeURIComponent("" + id)); 

        var content = "";

        jQuery.ajax({
            url: url,
            beforeSend: this.beforeSend,
            type: "get",
            data: content,
            dataType: "text",
            headers: {
                "Content-Type": "application/json; charset=UTF-8"
            }
        }).done((data, textStatus, xhr) => {
            this.processGetNameWithCallbacks(url, xhr, onSuccess, onFail);
        }).fail((xhr) => {
            this.processGetNameWithCallbacks(url, xhr, onSuccess, onFail);
        });
    }

    private processGetNameWithCallbacks(url: string, xhr: any, onSuccess?: any, onFail?: any) {
        try {
            var result = this.processGetName(xhr);
            if (onSuccess !== undefined)
                onSuccess(result);
        } catch (e) {
            if (onFail !== undefined)
                onFail(e, "http_service_exception");
        }
    }

    private processGetName(xhr: any) {
        var data = xhr.responseText; 
        var status = xhr.status.toString(); 

        if (status === "200") {
            var result200: string = null; 
            if (data !== undefined && data !== null && data !== "") {
                var resultData200 = data === "" ? null : jQuery.parseJSON(data);
                result200 = resultData200 !== undefined ? resultData200 : null;
            }
            return result200;
        }
        else
        {
            throw new Error("error_no_callback_for_the_received_http_status");
        }
    }
}

export class Person { 
    id: string; 
    /** Gets or sets the first name. */
    firstName: string; 
    /** Gets or sets the last name. */
    lastName: string; 
    gender: GenderAsInteger; 
    dateOfBirth: Date; 
    weight: number; 
    height: number; 
    age: number; 
    address: Address = new Address(); 
    children: Person[] = []; 
    skills: { [key: string] : SkillLevelAsInteger; }; 
    protected discriminator: string;

    constructor(data?: any) {
        this.discriminator = "Person";
        if (data !== undefined) {
            this.id = data["Id"] !== undefined ? data["Id"] : null;
            this.firstName = data["FirstName"] !== undefined ? data["FirstName"] : null;
            this.lastName = data["LastName"] !== undefined ? data["LastName"] : null;
            this.gender = data["Gender"] !== undefined ? data["Gender"] : null;
            this.dateOfBirth = data["DateOfBirth"] ? new Date(data["DateOfBirth"].toString()) : null;
            this.weight = data["Weight"] !== undefined ? data["Weight"] : null;
            this.height = data["Height"] !== undefined ? data["Height"] : null;
            this.age = data["Age"] !== undefined ? data["Age"] : null;
            this.address = data["Address"] ? Address.fromJS(data["Address"]) : new Address();
            if (data["Children"] && data["Children"].constructor === Array) {
                this.children = [];
                for (let item of data["Children"])
                    this.children.push(Person.fromJS(item));
            }
            if (data["Skills"]) {
                this.skills = {};
                for (let key in data["Skills"]) {
                    if (data["Skills"].hasOwnProperty(key))
                        this.skills[key] = data["Skills"][key] !== undefined ? data["Skills"][key] : null;
                }
            }
            this.discriminator = data["discriminator"] !== undefined ? data["discriminator"] : null;
        }
    }

    static fromJS(data: any): Person {
        if (data["discriminator"] === "Teacher")
            return new Teacher(data);
        return new Person(data);
    }

    toJS(data?: any) {
        data = data === undefined ? {} : data;
        data["Id"] = this.id !== undefined ? this.id : null;
        data["FirstName"] = this.firstName !== undefined ? this.firstName : null;
        data["LastName"] = this.lastName !== undefined ? this.lastName : null;
        data["Gender"] = this.gender !== undefined ? this.gender : null;
        data["DateOfBirth"] = this.dateOfBirth ? this.dateOfBirth.toISOString() : null;
        data["Weight"] = this.weight !== undefined ? this.weight : null;
        data["Height"] = this.height !== undefined ? this.height : null;
        data["Age"] = this.age !== undefined ? this.age : null;
        data["Address"] = this.address ? this.address.toJS() : null;
        if (this.children && this.children.constructor === Array) {
            data["Children"] = [];
            for (let item of this.children)
                data["Children"].push(item.toJS());
        }
        if (this.skills) {
            data["Skills"] = {};
            for (let key in this.skills) {
                if (this.skills.hasOwnProperty(key))
                    data["Skills"][key] = this.skills[key] !== undefined ? this.skills[key] : null;
            }
        }
        data["discriminator"] = this.discriminator !== undefined ? this.discriminator : null;
        return data; 
    }

    toJSON() {
        return JSON.stringify(this.toJS());
    }

    clone() {
        var json = this.toJSON();
        return new Person(JSON.parse(json));
    }
}

export class Teacher extends Person { 
    course: string;

    constructor(data?: any) {
        super(data);
        this.discriminator = "Teacher";
        if (data !== undefined) {
            this.course = data["Course"] !== undefined ? data["Course"] : null;
        }
    }

    static fromJS(data: any): Teacher {
        return new Teacher(data);
    }

    toJS(data?: any) {
        data = data === undefined ? {} : data;
        data["Course"] = this.course !== undefined ? this.course : null;
        super.toJS(data);
        return data; 
    }

    toJSON() {
        return JSON.stringify(this.toJS());
    }

    clone() {
        var json = this.toJSON();
        return new Teacher(JSON.parse(json));
    }
}

export enum GenderAsInteger {
    Male = 0, 
    Female = 1, 
}

export class Address { 
    isPrimary: boolean; 
    city: string;

    constructor(data?: any) {
        if (data !== undefined) {
            this.isPrimary = data["IsPrimary"] !== undefined ? data["IsPrimary"] : null;
            this.city = data["City"] !== undefined ? data["City"] : null;
        }
    }

    static fromJS(data: any): Address {
        return new Address(data);
    }

    toJS(data?: any) {
        data = data === undefined ? {} : data;
        data["IsPrimary"] = this.isPrimary !== undefined ? this.isPrimary : null;
        data["City"] = this.city !== undefined ? this.city : null;
        return data; 
    }

    toJSON() {
        return JSON.stringify(this.toJS());
    }

    clone() {
        var json = this.toJSON();
        return new Address(JSON.parse(json));
    }
}

export enum SkillLevelAsInteger {
    Low = 0, 
    Medium = 1, 
    Height = 2, 
}
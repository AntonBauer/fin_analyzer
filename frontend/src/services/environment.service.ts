import { Injectable } from '@angular/core';
import { environment } from '../environments/environment';
import { Environment } from '../environments/environment.interface';

@Injectable({
    providedIn: 'root',
})
export class EnvironmentService {
    getEnvironment(): Environment {
        return environment
    }
}
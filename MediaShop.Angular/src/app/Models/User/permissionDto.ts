import { Permissions } from './permissions';
import { Observable } from 'rxjs/Observable';

export class PermissionDto {
    Id: number;
    Permissions: Permissions;
    Login: string;
    Email: string;
    IsBanned: boolean;
    IsDeleted: boolean;

}


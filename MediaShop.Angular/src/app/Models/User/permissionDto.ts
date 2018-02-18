import { Permissions } from './permissions';

export class PermissionDto {
    Id: number;
    Permission: Permissions;
    Login: string;
    Email: string;
    IsBanned: boolean;
}

export enum Status {
    Warning,
    Success,
    Error
}

export interface BaseResponseModel {
    status: Status;
    message: string | null;
}

export interface BaseDataResponseModel<T> extends BaseResponseModel {
    data: T | null;
}
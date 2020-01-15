import { http } from "client/api";

export const setHeading = ({
	shipId,
	heading
}: {
	shipId: string;
	heading: number;
}) => http.post(`api/ship/${shipId}/heading/${heading}`);

export const setSpeed = ({
	shipId,
	speed
}: {
	shipId: string;
	speed: number;
}) => http.post(`api/ship/${shipId}/speed/${speed}`);

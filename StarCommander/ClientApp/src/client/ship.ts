import { http } from "client/api";

export const setHeading = ({
	shipId,
	heading,
	token
}: {
	shipId: string;
	heading: number;
	token: string;
}) => http.post(`api/ship/${shipId}/heading/${heading}`, {}, token);

export const setSpeed = ({
	shipId,
	speed,
	token
}: {
	shipId: string;
	speed: number;
	token: string;
}) => http.post(`api/ship/${shipId}/speed/${speed}`, {}, token);

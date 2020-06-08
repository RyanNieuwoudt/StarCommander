import { useEffect } from "react";
import { useHistory } from "react-router-dom";

export default function RedirectOnce({ to }) {
	const history = useHistory();
	useEffect(() => {
		if (history.location.pathname !== to) {
			history.push(to);
		}
	}, [history, to]);

	return null;
}

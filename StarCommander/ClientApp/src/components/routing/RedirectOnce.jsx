import { useEffect } from "react";
import { useRouter } from "hooks";

export default function RedirectOnce({ to }) {
	const router = useRouter();
	useEffect(() => {
		if (router.history.location.pathname !== to) {
			router.history.push(to);
		}
	}, [router, to]);

	return null;
}

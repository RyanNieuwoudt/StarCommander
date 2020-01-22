import React from "react";
import { useSelector } from "react-redux";
import { acknowledgement } from "selectors";

export default function Acknowledgements() {
	const acknowledgements = useSelector(acknowledgement);

	return (
		<ul>
			{acknowledgements.map((a, i) => (
				<li key={i}>{a}</li>
			))}
		</ul>
	);
}

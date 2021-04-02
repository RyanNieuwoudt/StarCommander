import React from "react";
import { Text } from "grommet";
import { useSelector } from "react-redux";
import { player } from "selectors";

export default function Name() {
	const { firstName, lastName } = useSelector(player) || {
		firstName: "",
		lastName: "",
	};

	return (
		<Text>
			{firstName} {lastName}
		</Text>
	);
}

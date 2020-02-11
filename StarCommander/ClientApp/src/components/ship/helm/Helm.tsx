import React, { useCallback, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import { Box, Button } from "grommet";
import { usePrevious } from "hooks";
import { shipHeading, shipId as shipIdSelector, shipSpeed } from "selectors";
import { actionCreators } from "store/ship";
import { Heading, Speed } from ".";

interface SetNumber {
	target: { value: React.SetStateAction<number> };
}

export default function Helm() {
	const dispatch = useDispatch();

	const shipId = useSelector(shipIdSelector);
	const heading = useSelector(shipHeading);
	const speed = useSelector(shipSpeed);

	const [newHeading, setNewHeading] = useState(heading);
	const [newSpeed, setNewSpeed] = useState(speed);

	const previousHeading = usePrevious(heading);
	if (previousHeading !== heading && newHeading !== heading) {
		setNewHeading(heading);
	}

	const previousSpeed = usePrevious(speed);
	if (previousSpeed !== speed && newSpeed !== speed) {
		setNewSpeed(speed);
	}

	const engage = useCallback(() => {
		if (!shipId) {
			return;
		}

		if (newHeading !== heading) {
			dispatch(actionCreators.setHeading(shipId, newHeading));
		}

		if (newSpeed !== speed) {
			dispatch(actionCreators.setSpeed(shipId, newSpeed));
		}
	}, [dispatch, newHeading, newSpeed, shipId]);

	return (
		<Box gap="medium" round="medium">
			<Heading
				value={newHeading}
				onChange={(event: SetNumber) =>
					setNewHeading(event.target.value)
				}
			/>
			<Speed
				value={newSpeed}
				onChange={(event: SetNumber) => setNewSpeed(event.target.value)}
			/>
			<Button label="Engage" primary onClick={engage} />
		</Box>
	);
}

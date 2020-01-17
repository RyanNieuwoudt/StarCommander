import * as React from "react";
import { Box } from "grommet";
import { Layout } from "components";
import { UpdateNameForm } from "components/player";

export default function Fleet() {
	return (
		<Layout>
			<Box align="center" pad="large">
				<UpdateNameForm />
			</Box>
		</Layout>
	);
}

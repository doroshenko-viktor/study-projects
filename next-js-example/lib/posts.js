import path from 'path';
import matter from 'gray-matter';
import fs from 'fs';

const currentDir = process.cwd();
const postsDir = path.join(currentDir, 'posts');

export function getSortedPostsData() {
    const fileNames = fs.readdirSync(postsDir);
    const allPostsData = fileNames.map(fileName => {
        const id = fileName.replace(/\.md$/, '');

        const fullPath = path.join(postsDir, fileName);
        const fileContents = fs.readFileSync(fullPath, 'utf8');

        const matterResult = matter(fileContents);

        return { id, ...matterResult.data };
    })

    return allPostsData.sort(({ date: a }, { date: b }) => {
        if (a < b) {
            return 1
        } else if (a > b) {
            return -1
        } else {
            return 0
        }
    })
}